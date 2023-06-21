using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Uno.Wasm.Sample
{
	public static class Program
	{
		static async Task Main(string[] args)
		{
			Directory.CreateDirectory("/tmp");
			await File.WriteAllTextAsync("/tmp/test.txt", "test");
			await File.ReadAllBytesAsync("/tmp/test.txt");
		}
	}

	public static partial class Import
	{
		[System.Runtime.InteropServices.JavaScript.JSImport("globalThis.Test")]
		public static partial void Test();
	}
}