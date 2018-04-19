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
				// Console.WriteLine($"Got compilation DeclarationDiagnostics:{compilation.GetDiagnostics().Length}");

				// GetDeclarationDiagnostics fails with the following error:
				//
				// Failed to load project: System.OutOfMemoryException: Out of memory
				// mono.js:1   at System.Collections.Immutable.ImmutableArray.CreateRange[TSource, TResult](System.Collections.Immutable.ImmutableArray`1[T] items, System.Func`2[T, TResult] selector) < 0x3ff40b8 + 0x00042 > in < e8fa49cb4bda44449727c67d098f8723 >:0
				// mono.js:1   at Microsoft.CodeAnalysis.ImmutableArrayExtensions.SelectAsArray[TItem, TResult](System.Collections.Immutable.ImmutableArray`1[T] items, System.Func`2[T, TResult] map) < 0x3ff3e68 + 0x00018 > in < 14c57b2ce18d4fa4ae73754053c8ab8b >:0
				// mono.js:1   at Microsoft.CodeAnalysis.CSharp.Symbols.CSharpCustomModifier.Convert(System.Collections.Immutable.ImmutableArray`1[T] customModifiers) < 0x3ff3cf8 + 0x00034 > in < ccc9a15bbeb940fd83da7e96bb05fdc4 >:0
				// mono.js:1   at Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE.PEParameterSymbol + PEParameterSymbolWithCustomModifiers..ctor(Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE.PEModuleSymbol moduleSymbol, Microsoft.CodeAnalysis.CSharp.Symbol containingSymbol, System.Int32 ordinal, System.Boolean isByRef, System.Collections.Immutable.ImmutableArray`1[T] refCustomModifiers, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, System.Reflection.Metadata.ParameterHandle handle, System.Collections.Immutable.ImmutableArray`1[T] customModifiers, System.Boolean & isBad) < 0x3ff37f0 + 0x000da > in < ccc9a15bbeb940fd83da7e96bb05fdc4 >:0
				//
				Console.WriteLine($"Got compilation DeclarationDiagnostics:{compilation.GetDeclarationDiagnostics().Length}");

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
					allowUnsafe: true);

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
