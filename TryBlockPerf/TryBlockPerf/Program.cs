using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WebAssembly.JSInterop;

namespace StructLayout
{
	public class Program
	{
        private const int Iterations = 1000000;

        static int Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                var s1 = Test01();
                var s2 = Test02();
                var s3 = Test03();
                var s4 = Test04();
                var s5 = Test05();

                Console.WriteLine($"s1={s1} s2={s2} s3={s3} s4={s4} s5={s5}");
            }
            return 0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void MyWorkload()
        {

        }

        private static TimeSpan Test01()
        {
            var sw = Stopwatch.StartNew();

            var isSet = false;
            int counter = 0;
            void MyMethod()
            {
                isSet = true;
                counter++;
                MyWorkload();
                isSet = false;
            }

            for (int i = 0; i < Iterations; i++)
            {
                MyMethod();
            }

            sw.Stop();

            return sw.Elapsed;
        }

        private static TimeSpan Test02()
        {
            var sw = Stopwatch.StartNew();

            var isSet = false;
            int counter = 0;
            void MyMethod()
            {
                try
                {
                    isSet = true;
                    MyWorkload();
                    counter++;
                }
                finally
                {
                    isSet = false;
                }
            }

            for (int i = 0; i < Iterations; i++)
            {
                MyMethod();
            }

            sw.Stop();

            return sw.Elapsed;
        }

        private static TimeSpan Test03()
        {
            var sw = Stopwatch.StartNew();

            var isSet = false;
            int counter = 0;
            void MyMethod()
            {
                try
                {
                    isSet = true;
                    MyWorkload();
                    MyWorkload();
                    counter++;
                }
                finally
                {
                    isSet = false;
                }
            }

            for (int i = 0; i < Iterations; i++)
            {
                MyMethod();
            }

            sw.Stop();

            return sw.Elapsed;
        }

        private static IDisposable CreateDisposable()
        {
            return null;
        }

        private static TimeSpan Test04()
        {
            var sw = Stopwatch.StartNew();

            var isSet = false;
            int counter = 0;
            void MyMethod()
            {
                try
                {
                    isSet = true;
                    using (CreateDisposable())
                    {
                        MyWorkload();
                        MyWorkload();
                        counter++;
                    }
                }
                finally
                {
                    isSet = false;
                }
            }

            for (int i = 0; i < Iterations; i++)
            {
                MyMethod();
            }

            sw.Stop();

            return sw.Elapsed;
        }

        private static TimeSpan Test05()
        {
            var sw = Stopwatch.StartNew();

            var isSet = false;
            int counter = 0;
            void MyMethod()
            {
                isSet = true;
                counter++;
                MyWorkload();
                MyWorkload();
                isSet = false;
            }

            for (int i = 0; i < Iterations; i++)
            {
                MyMethod();
                MyMethod();
            }

            sw.Stop();

            return sw.Elapsed;
        }

    }
}
