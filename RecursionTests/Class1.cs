using System;
using System.Threading;

namespace WasmTimerTests
{
    public class Class1
    {
        static void Main(string[] args)
        {
            // Using Safari for iOS 12.1.2, with the interpreter
            // 
            // SmallStackFailure(1000);             // 01f6a02: 116 levels deep -> 7952489bd2c: 129 levels deep
            // SmallStackFailure(5000);             // 01f6a02: 790 levels deep ->  7952489bd2c: 918 levels deep
            // SmallStackFailureDelegates(1000);    // 01f6a02: 57 levels deep -> 7952489bd2c: 64 levels deep
            SmallStackFailureDelegates(2000);       // 01f6a02: 390 levels deep ->  7952489bd2c: 460 levels deep
        }

        private static void SmallStackFailureDelegates(int delay)
        {
            int run = 0;

            Timer t = null;

            t = new Timer(_ =>
            {

                if (run++ < 1)
                {
                    Console.WriteLine($"Skip");
                    t.Change(delay, -1);
                }
                else
                {

                    Action<int> recurse = null;

                    recurse = (int level) =>
                    {
                        Console.WriteLine($"level {level}");

                        //if (level == 110)
                        //{
                        //    WebAssembly.Runtime.InvokeJS("debugger;");
                        //}

                        if (level + 1 < 1000)
                        {
                            recurse(level + 1);
                        }
                        else
                        {
                        }
                    };

                    recurse(0);
                }
            });

            t.Change(delay, -1);
        }

        private static void SmallStackFailure(int delay)
        {
            int run = 0;

            Timer t = null;

            t = new Timer(_ =>
            {

                if (run++ < 1)
                {
                    Console.WriteLine($"Skip");
                    t.Change(delay, -1);
                }
                else
                {

                    void recurse(int level)
                    {
                        Console.WriteLine($"level {level}");

                        //if (level == 110)
                        //{
                        //    WebAssembly.Runtime.InvokeJS("debugger;");
                        //}

                        if (level + 1 < 1000)
                        {
                            recurse(level + 1);
                        }
                        else
                        {
                        }
                    }

                    recurse(0);
                }
            });

            t.Change(delay, -1);
        }
    }
}
