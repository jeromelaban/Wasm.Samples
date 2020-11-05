using System;
using System.Diagnostics;
using System.Reflection;
using Uno.Extensions;
using Windows.UI.Xaml;

namespace App67.Wasm
{
	public class Program
	{
		private static App _app;

		static int Main(string[] args)
		{
			var u = new Util();
			u.Use();

			var asm = Assembly.Load("Uno.UI, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null");
			Console.WriteLine($"Assembly1: {asm}");

			Windows.UI.Xaml.Application.Start(_ => _app = new App());

			return 0;
		}
	}

	unsafe class Util
	{
		public static void Log() { }
		public static void Log(string p1) { }
		public static void Log(int i) { }

		public void Use()
		{
			delegate*<void> a1 = &Log;

			a1();
		}
	} 
}
