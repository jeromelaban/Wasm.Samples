
using System;

namespace WebAssembly
{
	internal sealed class Runtime
	{
		[System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.InternalCall)]
		private static extern string InvokeJS(string str, out int exceptional_result);

		internal static string InvokeJS(string str)
		{
			var r = InvokeJS(str, out var exceptionResult);
			if (exceptionResult != 0)
			{
				Console.Error.WriteLine($"Error #{exceptionResult} \"{r}\" executing javascript: \"{str}\"");
			}
			return r;
		}
	}
}
