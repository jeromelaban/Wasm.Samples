using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace RoslynTests
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Rolsyn test");

			var sf = SyntaxFactory.ParseCompilationUnit("public class Program { public static void Main(){ Console.WriteLine(\"Inner hello!\"); } }");
			Console.WriteLine($"Parsed SyntaxNode: {sf}");

			try
			{
				// Does not work yet, something's missing in System.Composition support for interpreted compiled expression.
				var p = CreateProject(new[] { "public class Program { public static void Main(){ Console.WriteLine(\"Inner hello!\"); } }" });
			}
			catch (Exception e)
			{
				Console.WriteLine($"Failed to load project: {e}");
			}
		}

		internal static string DefaultFilePathPrefix = "Test";

		internal static string CSharpDefaultFileExt = "cs";

		internal static string VisualBasicDefaultExt = "vb";

		internal static string TestProjectName = "TestProject";

		private static readonly MetadataReference CorlibReference =
			MetadataReference.CreateFromFile(typeof(object).Assembly.Location);

		private static readonly MetadataReference SystemCoreReference =
			MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location);

		private static readonly MetadataReference CodeAnalysisReference =
			MetadataReference.CreateFromFile(typeof(Compilation).Assembly.Location);

		private static Project CreateProject(string[] sources, string language = LanguageNames.CSharp)
		{
			var fileNamePrefix = ".cs";

			var fileExt = language == LanguageNames.CSharp ? CSharpDefaultFileExt : VisualBasicDefaultExt;

			var projectId = ProjectId.CreateNewId(TestProjectName);

			var solution = new AdhocWorkspace()
				.CurrentSolution
				.AddProject(projectId, TestProjectName, TestProjectName, language)
				.AddMetadataReference(projectId, CorlibReference)
				.AddMetadataReference(projectId, SystemCoreReference)
				.AddMetadataReference(projectId, CodeAnalysisReference);

			var count = 0;

			foreach (var source in sources)
			{
				var newFileName = fileNamePrefix + count + "." + fileExt;
				var documentId = DocumentId.CreateNewId(projectId, newFileName);
				solution = solution.AddDocument(documentId, newFileName, SourceText.From(source));
				count++;
			}

			return solution.GetProject(projectId);

		}
	}
}
