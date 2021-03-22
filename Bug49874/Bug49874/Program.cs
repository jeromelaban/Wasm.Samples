using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("test")]

static class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("Listing for Program");

		foreach (var t in typeof(Program).Assembly.GetCustomAttributes(true))
		{
			Console.WriteLine(t);
		}

		Console.WriteLine("Listing for ClassLibrary1.Class1");

		// Listing those attributes fails
		foreach (var t in typeof(ClassLibrary1.Class1).Assembly.GetCustomAttributes(true))
		{
			Console.WriteLine(t);
		}

		Console.WriteLine("Done");
	}
}

