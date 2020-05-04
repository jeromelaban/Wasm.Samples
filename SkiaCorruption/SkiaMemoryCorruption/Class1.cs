using System;
using System.IO;
using System.Runtime.InteropServices;
using SkiaSharp;

namespace SkiaMemoryCorruption
{
	public class Program
	{
		[DllImport("myclib")]
		private static extern void drawLine(IntPtr ptr, float x0, float y0, float x1, float y1, IntPtr ptr2);

		static int Main(string[] args)
		{
			using (var reader = new StringReader(""))
			{
				for (int i = 0; i < 1000; i++)
				{
					for (double d = 0; d < 1000; d += 5)
						drawLine(IntPtr.Zero, 0, 0, 1, 1, IntPtr.Zero);

					for (double d = 0; d < 1000; d += 5)
						drawLine(IntPtr.Zero, 0, 0, 1, 1, IntPtr.Zero);
				}
			}

			return 0;
		}
	}
}
