using Bug16818;
using System;
using System.Collections.Generic;
using System.Threading;

namespace WasmTimerTests
{
    public class Class1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("s1");
            var r = new IDisposable[1];
            Console.WriteLine("s2");
            LoggerFactory.Test();
            Console.WriteLine("s3");
            var logger = new LoggerFactory();
            Console.WriteLine("s4");
       }
    }
}
