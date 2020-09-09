using System;
using System.Threading;

namespace WasmTimerTests
{
    public class Class1
    {

        static void Main(string[] args)
        {
			Action<int> a = i => { Console.WriteLine("test"); };
        }
    }
}
