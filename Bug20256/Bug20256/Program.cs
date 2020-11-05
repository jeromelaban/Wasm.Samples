using Bug20195Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace WasmTimerTests
{
	public class Class1
	{
		static void Main(string[] args)
		{
			Console.WriteLine($"{typeof(ResourceResolver).Assembly}");

			var asm1 = "Uno.UI, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null";
			var asm2 = typeof(ResourceResolver).Assembly.FullName;

			var asm1dump = string.Join(",", asm1.Select(c => $"{(int)c:X4}"));

			//Console.WriteLine($"asm1: {asm1}[{asm1.Length}]"); // 67 chars
			//Console.WriteLine($"asm2: {asm2}[{asm2.Length}]"); // 68 chars ??
			Console.WriteLine($"asm1dump: {asm1dump}");

			//Console.WriteLine(Assembly.Load(asm2)); // this works
			//Console.WriteLine(Assembly.Load(asm1)); // this fails

			Console.WriteLine("starting");
		}

		public static void Link()
		{
			new ResourceResolver2();
		}
	}

	public class ResourceResolver2 : ResourceResolver
	{
		public ResourceResolver2()
		{
			NewMethod();
		}

		private  void NewMethod()
		{
			var asm = Assembly.Load(typeof(ResourceResolver).Assembly.FullName);
			Console.WriteLine($"asm: {asm}"); // this works
		}
	}
}
