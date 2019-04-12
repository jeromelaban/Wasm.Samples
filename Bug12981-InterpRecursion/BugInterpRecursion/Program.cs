using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace WasmTimerTests
{
    public class Class1
    {

        static void Main(string[] args)
        {
            LoadTest();
        }

        private static void LoadTest()
        {
            Type.GetType("WasmTimerTests.Exports").GetMethod("Test2").Invoke(null, new object[0]);
        }
    }

    public static class Exports
    {
        [DllImport("canvaskit", CallingConvention = CallingConvention.Cdecl)]
        public extern static void sk_color_get_bit_shift(out int a, out int r, out int g, out int b);

        public static void Test2()
        {
            sk_color_get_bit_shift(out var a, out var r, out var g, out var b);

            Console.WriteLine($"a:{a} r:{r} g:{g} b:{b}");
        }
    }
}
