using Bug20195Library;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace WasmTimerTests
{
    public class Class1
    {
        static void Main(string[] args)
        {
			Link();
			Console.WriteLine($"{typeof(ResourceResolver).Assembly}");

            var asm1 = "﻿OtherLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
			var asm2 = typeof(ResourceResolver).Assembly.FullName;

            Console.WriteLine($"asm1: {asm1}[{asm1.Length}]"); // 67 chars
            Console.WriteLine($"asm2: {asm2}[{asm2.Length}]"); // 68 chars ??

            Console.WriteLine(Assembly.Load(asm2)); // this works
            Console.WriteLine(Assembly.Load(asm1)); // this fails
        }

        public static void Link()
		{
            new ResourceResolver();
        }
    }
}
