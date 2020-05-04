using System;
using System.Runtime.InteropServices;

namespace DelegateThunk
{
    public class Program
    {
		static void Main(string[] args)
		{
			Action a = () => Console.WriteLine("Test callback!");

			var ptr = Marshal.GetFunctionPointerForDelegate(a);

			WebAssembly.Runtime.InvokeJS($"testInvoke({ptr})");
		}
    }
}
