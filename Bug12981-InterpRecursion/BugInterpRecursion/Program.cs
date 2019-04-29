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
            using (var s = typeof(Class1).Assembly.GetManifestResourceStream("BugInterpRecursion.side.wasm"))
            {
                using (var o = File.OpenWrite("./side.wasm"))
                {
                    s.CopyTo(o);
                }
            }

            using (var s = typeof(Class1).Assembly.GetManifestResourceStream("BugInterpRecursion.canvaskit.wasm"))
            {
                using (var o = File.OpenWrite("./canvaskit.wasm"))
                {
                    s.CopyTo(o);
                }
            }

            LoadTest();
        }

        private static void LoadTest()
        {
            // Type.GetType("WasmTimerTests.Exports").GetMethod("InvokeMyMethod").Invoke(null, new object[0]);
            Type.GetType("WasmTimerTests.Exports").GetMethod("Test2").Invoke(null, new object[0]);
        }
    }

    public static class Exports
    {
        [DllImport("side")]
        public extern static int test_add(int a, int b);

        public static void InvokeMyMethod()
        {
            Console.WriteLine("test_add: " + test_add(21, 21));
        }

        [DllImport("canvaskit")]
        public extern static void sk_color_get_bit_shift(out int a, out int r, out int g, out int b);

        public static void Test2()
        {
            sk_color_get_bit_shift(out var a, out var r, out var g, out var b);

            Console.WriteLine($"a:{a} r:{r} g:{g} b:{b}");
        }
    }
}
