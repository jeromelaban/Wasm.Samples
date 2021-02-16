using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

static class Program
{
	static void Main(string[] args)
	{
		var r = MyMethod("42");

		// Second invocation should show "value: 1 length:1", but shows "value: length:"
		var r2 = MyMethod("1");
	}

	private static string MyMethod(string test)
		=> WebAssembly.Runtime.InvokeJS($"Module.mono_bind_static_method(\"[Bug48345] Program:TestInterop\")(\"{test}\")");

	public static string TestInterop(string value)
	{
		Console.WriteLine($"value: {value} length: {value?.Length}");
		return "1";
	}
}

