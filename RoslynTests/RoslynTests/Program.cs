using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Host;
using Microsoft.CodeAnalysis.Text;

namespace RoslynTests
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			Console.WriteLine("Rolsyn test");

			var sf = SyntaxFactory.ParseCompilationUnit("public class Program { public static void Main(){ System.Console.WriteLine(\"Inner hello!\"); } }");
			Console.WriteLine($"Parsed SyntaxNode: {sf}");

			try
			{
				var sourceLanguage = new CSharpLanguage();

				Compilation compilation = sourceLanguage
				  .CreateLibraryCompilation(assemblyName: "InMemoryAssembly", enableOptimisations: false)
				  .AddSyntaxTrees(new[] { sf.SyntaxTree });

				Console.WriteLine($"Got compilation {File.ReadAllBytes(CorlibReference.Display)?.Length}");

				Console.WriteLine($"Symbol:{ compilation.GetTypeByMetadataName("System.Console")}");

				// GetDiagnostics seems to keep running in a CPU Bound loop
				Console.WriteLine($"Got compilation Diagnostics: {compilation.GetDiagnostics().Length}");
				Console.WriteLine($"Got compilation DeclarationDiagnostics: {compilation.GetDeclarationDiagnostics().Length}");

				Console.WriteLine($"Emitting assembly...");
				var stream = new MemoryStream();
				var emitResult = compilation.Emit(stream);

				if (emitResult.Success)
				{
					Console.WriteLine($"Got binary assembly: {emitResult.Success}");

					var asm = Assembly.Load(stream.ToArray());
					if (asm.GetExportedTypes().Where(et => et.Name == "Program").FirstOrDefault() is Type programType)
					{
						Console.WriteLine("Got Program type");
						if (programType.GetMethod("Main") is MethodInfo mainMethod)
						{
							Console.WriteLine("Got Main method");
							mainMethod.Invoke(null, null);
						}
					}
				}
				else
				{
					Console.WriteLine($"Failed to emit assembly:");

					foreach (var diagnostic in emitResult.Diagnostics)
					{
						Console.WriteLine(diagnostic);
					}
				}
			}
			catch (ReflectionTypeLoadException e)
			{
				Console.WriteLine($"TypeLoaderException: {string.Join("\n", e.LoaderExceptions.Select(i => i.ToString()))}");
			}
			catch (Exception e)
			{
				Console.WriteLine($"Failed to load project: {e}");
			}
		}

		public class CSharpLanguage : ILanguageService
		{
			private readonly IReadOnlyCollection<MetadataReference> _references = new[] {
				CorlibReference,
				SystemCoreReference,
				ConsoleReference,
				DecimalReference,
			};


		public Compilation CreateLibraryCompilation(string assemblyName, bool enableOptimisations)
			{
				var options = new CSharpCompilationOptions(
					OutputKind.DynamicallyLinkedLibrary,
					optimizationLevel: OptimizationLevel.Release,
					allowUnsafe: true)
					// Disabling concurrent builds allows for the emit to finish.
					.WithConcurrentBuild(false)
					;

				Console.WriteLine($"References: {string.Join(", ", _references.Select(r => r.Display))}");

				return CSharpCompilation.Create(assemblyName, options: options, references: _references);
			}
		}

		private static readonly MetadataReference CorlibReference =
			MetadataReference.CreateFromFile(typeof(object).Assembly.Location);

		private static readonly MetadataReference ConsoleReference =
			MetadataReference.CreateFromFile(typeof(System.Console).Assembly.Location);

		private static readonly MetadataReference DecimalReference =
			MetadataReference.CreateFromFile(typeof(System.Decimal).Assembly.Location);

		private static readonly MetadataReference SystemCoreReference =
			MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location);

		private static readonly MetadataReference CodeAnalysisReference =
			MetadataReference.CreateFromFile(typeof(Compilation).Assembly.Location);
	}
}
