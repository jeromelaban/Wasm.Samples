using System;
using System.Threading;

namespace WasmTimerTests
{
    public class Class1
    {
        static void Main(string[] args) => MethodA(null);

        internal static bool MethodA(object sender)
        {
            if (sender == null) { return true; }
            return MethodA(sender);
        }
    }
}
