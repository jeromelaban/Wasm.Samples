using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WebAssembly.JSInterop;

namespace StructLayout
{
	public class Program
	{
		static int Main(string[] args)
        {
            InvokeFast(0);
            InvokeSlow(0);

            WebAssembly.Runtime.InvokeJS($"showResult(\"Starting... 2\")");

            var sw1 = FastLoop();

            var sw2 = SlowLoop();

            var sw3 = FastLoopById();

            var message = 
                $"JSInvoke: {sw2.Elapsed} " +
                $"EM_JS: {sw1.Elapsed} " +
                $"EM_JS_ID: {sw3.Elapsed}";
            WebAssembly.Runtime.InvokeJS($"showResult(\"{message}\")");

            return 0;
        }

        private static Stopwatch FastLoopById()
        {
            var sw3 = Stopwatch.StartNew();

            var testLayoutFastId = InternalCalls.InvokeJSUnmarshalled<object, object, object, IntPtr>(out var exception, $"register:testLayoutFast", null, null, null);

            for (int i = 0; i < 10000; i++)
            {
                InvokeFastById(i, testLayoutFastId);
            }

            sw3.Stop();
            return sw3;
        }

        private static Stopwatch SlowLoop()
        {
            var sw2 = Stopwatch.StartNew();

            for (int i = 0; i < 10000; i++)
            {
                InvokeSlow(i);
            }

            sw2.Stop();
            return sw2;
        }

        private static Stopwatch FastLoop()
        {
            var sw1 = Stopwatch.StartNew();

            for (int i = 0; i < 10000; i++)
            {
                InvokeFast(i);
            }

            sw1.Stop();
            return sw1;
        }

        private static void InvokeFastById(int value, IntPtr testLayoutFastId)
        {
            var parms = new WindowManagerCreateContentParams
            {
                HtmlId = (IntPtr)value,
                TagName = value.ToString(),
                Handle = (IntPtr)44,
                Type = "MyType",
                IsSvg = true,
                IsFrameworkElement = false,
                IsFocusable = true,
                ClassesCount = 2,
                Classes = new[] { "class1", "class2" },
            };

            var pParms = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WindowManagerCreateContentParams)));

            try
            {
                Marshal.StructureToPtr(parms, pParms, false);

                var ret = InternalCalls.InvokeJSUnmarshalled<IntPtr, IntPtr, object, IntPtr>(out var exception, null, testLayoutFastId, pParms, null);

                if (ret != parms.HtmlId)
                {
                    throw new Exception($"Invalid fast response [{ret}]");
                }
            }
            finally
            {
                Marshal.DestroyStructure(pParms, typeof(WindowManagerCreateContentParams));
                // Free the unmanaged memory.
                Marshal.FreeHGlobal(pParms);
            }
        }

        private static void InvokeSlow(int value)
		{
			int HtmlId = value;
			string HtmlTag = value.ToString();
			int Handle = 42;
			string typeFullName = "type name";
			bool isSvgStr = true;
			bool isFrameworkElementStr = false;
			bool isFocusable = true;
			string classes = "\"class1\"";

			var ret = WebAssembly.Runtime.InvokeJS(
				"testLayoutSlow({" +
				"id:" + HtmlId + "," +
				"tagName:\"" + HtmlTag + "\", " +
				"handle:" + Handle + ", " +
				"type:\"" + typeFullName + "\", " +
				"isSvg:" + (isSvgStr ? "true" : "false") + ", " +
				"isFrameworkElement:" + (isFrameworkElementStr ? "true" : "false") + ", " +
				"isFocusable:" + (isFocusable ? "true" : "false") + ", " +
				"classes:[" + classes + "]" +
				"})");

			if (int.TryParse(ret, out var result) && result != HtmlId)
			{
				throw new Exception($"Invalid slow response ({result})");
			}
		}

		private static void InvokeFast(int value)
		{
			var parms = new WindowManagerCreateContentParams
			{
				HtmlId = (IntPtr)value,
				TagName = value.ToString(),
				Handle = (IntPtr)44,
				Type = "MyType",
				IsSvg = true,
				IsFrameworkElement = false,
				IsFocusable = true,
				ClassesCount = 2,
				Classes = new[] { "class1", "class2" },
			};

			var pParms = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WindowManagerCreateContentParams)));

			try
			{
				Marshal.StructureToPtr(parms, pParms, false);

				var ret = InternalCalls.InvokeJSUnmarshalled<IntPtr, object, object, IntPtr>(out var exception, $"testLayoutFast", pParms, null, null);

				if (ret != parms.HtmlId)
				{
					throw new Exception($"Invalid fast response [{ret}]");
				}
			}
			finally
			{
				Marshal.DestroyStructure(pParms, typeof(WindowManagerCreateContentParams));
				// Free the unmanaged memory.
				Marshal.FreeHGlobal(pParms);
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct WindowManagerCreateContentParams
		{
			public IntPtr HtmlId;

			[MarshalAs(48/* LPUTF8Str */)]
			public string TagName;

			public IntPtr Handle;

			[MarshalAs(48/* LPUTF8Str */)]
			public string Type;

			public bool IsSvg;
			public bool IsFrameworkElement;
			public bool IsFocusable;

			public int ClassesCount;

			[MarshalAs(UnmanagedType.LPArray, ArraySubType = (UnmanagedType)48/* LPUTF8Str */)]
			public string[] Classes;
		}
	}
}
