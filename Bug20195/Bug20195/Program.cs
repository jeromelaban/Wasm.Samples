using Bug20195Library;
using System;
using System.Collections.Generic;
using System.Threading;

namespace WasmTimerTests
{
    public class Class1
    {
        static void Main(string[] args)
        {
            new MyOtherTestClass().Test();
            new MyLibraryTestClass().Test();
        }
    }

    public class MyOtherTestClass
    {
        static int c_paneToggleButtonWidth = 48;

        public void Test()
        {
            var buttonSize = ResourceResolver.ResolveTopLevelResource<double>(key: "PaneToggleButtonWidth", fallbackValue: c_paneToggleButtonWidth);
        }
    }
}
