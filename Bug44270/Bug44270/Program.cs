using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

InternalImportTest.mono_trace_enable(1);

System.Console.WriteLine("Test");

var CommentStripperRegex = new Regex(@"(/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/)|(//.*)");

static class InternalImportTest
{
	[DllImport("__Native")]
	internal static extern void mono_trace_enable(int enable);
}