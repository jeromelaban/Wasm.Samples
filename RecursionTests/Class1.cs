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
            // SmallStackFailure(1000); // This one fails around 116 levels deep
            // SmallStackFailure(5000); // This one fails around 790 levels deep
            // SmallStackFailureDelegates(1000); // this one fails at 57 levels deep
            SmallStackFailureDelegates(2000); // this one fails at 390 levels deep
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
