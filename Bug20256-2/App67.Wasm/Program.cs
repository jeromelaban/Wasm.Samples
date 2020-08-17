using System;
using System.Reflection;
using Windows.UI.Xaml;

namespace App67.Wasm
{
	public class Program
	{
		private static App _app;

		static int Main(string[] args)
		{
			var asm = Assembly.Load("Uno.UI, Version=255.255.255.255, Culture=neutral, PublicKeyToken=null");
			Console.WriteLine($"Assembly1: {asm}");

			Windows.UI.Xaml.Application.Start(_ => _app = new App());

			return 0;
		}
	}
}
