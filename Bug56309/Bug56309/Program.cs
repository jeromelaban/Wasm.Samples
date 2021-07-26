using System;
using System.Diagnostics;

namespace Uno.Wasm.Sample
{
	public static class Program
	{
		static void Main(string[] args)
		{
			TestTryCatch();
		}

		private static void TestTryCatch()
		{
			int InvokeMethod(int a)
			{
				try
				{
					return a++;
				}
				catch
				{
					Console.WriteLine("catch");
					return 0;
				}
			}

			var sw = Stopwatch.StartNew();
			for (int i = 0; i < 10000000; i++)
			{
				InvokeMethod(i);
			}
			sw.Stop();
			Console.WriteLine($"TestTryCatch: {sw.ElapsedMilliseconds}");
		}
	}
}