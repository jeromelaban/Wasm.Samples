using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Uno.Wasm.Sample
{
    public static class Program
    {
        private static ManualResetEvent _event = new ManualResetEvent(false);
        private static object _gate = new object();
        private static List<string> _messages = new List<string>();
        private static bool _mainThreadInvoked = false;

        static void Main()
        {
            var runtimeMode = Environment.GetEnvironmentVariable("UNO_BOOTSTRAP_MONO_RUNTIME_MODE");
            Console.WriteLine($"Runtime Version: " + RuntimeInformation.FrameworkDescription);
            Console.WriteLine($"Runtime Mode: " + runtimeMode);
            Console.WriteLine($"TID: {Thread.CurrentThread.ManagedThreadId}");

            Bug01();
        }

        private static void Bug01()
        {
            Timer t = new Timer(_ =>
            {
                Log($"Before GC");
                GC.Collect();
                Log($"After GC");
            });
            t.Change(5000, 0);

            Log($"-> Bug01");
            Task.Run(async () =>
            {
                var sw = Stopwatch.StartNew();
                Log($"Before 2000ms delay");
                await Task.Delay(2000);
                Log($"After delay {sw.ElapsedMilliseconds}");
            });
            Log($"<- Bug01");

            void Log(string message) => Console.WriteLine($"[TID:{Thread.CurrentThread.ManagedThreadId}] {message}");
        }
    }
}

namespace WebAssembly.JSInterop
{
    public static class InternalCalls
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public static extern void InvokeOnMainThread();
    }
}
