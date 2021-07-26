using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Uno.Wasm.Sample
{
	public static class Program
	{
		static void Main(string[] args)
		{
			TestForeachListOfT();
			TestForListOfT();
			TestForArray();
		}

		private static void TestForArray()
		{
			var SUT = new List<object>();
			SUT.AddRange(Enumerable.Range(1, 1000)
				.Select(i => (object)null));
			var SUT2 = SUT.ToArray();

			var sw = Stopwatch.StartNew();

			for (int i = 0; i < 5000; i++)
			{
				int count = 0;
				foreach (var item in SUT2)
				{
					count++;
				}
			}

			sw.Stop();
			Console.WriteLine($"TestForArray: {sw.Elapsed}");
		}


		private static void TestForListOfT()
		{
			var SUT = new List<object>();
			SUT.AddRange(Enumerable.Range(1, 1000)
				.Select(i => (object)null));

			var sw = Stopwatch.StartNew();

			for (int j = 0; j < 5000; j++)
			{
				int count = 0;
				for (int i = 0; i < SUT.Count; i++)
				{
					count++;
				}
			}

			sw.Stop();
			Console.WriteLine($"TestForListOfT: {sw.Elapsed}");
		}

		private static void TestForeachListOfT()
		{
			var SUT = new List<object>();
			SUT.AddRange(Enumerable.Range(1, 1000)
				.Select(i => (object)null));

			var sw = Stopwatch.StartNew();

			for (int i = 0; i < 5000; i++)
			{
				int count = 0;
				foreach (var item in SUT)
				{
					count++;
				}
			}

			sw.Stop();
			Console.WriteLine($"TestForeachListOfT: {sw.Elapsed}");
		}
	}

	public partial struct Color : IFormattable
	{
		public byte A { get; set; }

		public byte B { get; set; }

		public byte G { get; set; }

		public byte R { get; set; }

		internal bool IsTransparent => A == 0;

		public static Color FromArgb(byte a, byte r, byte g, byte b) => new Color(a, r, g, b);

		private Color(byte a, byte r, byte g, byte b)
		{
			A = a;
			R = r;
			G = g;
			B = b;
		}

		public override bool Equals(object o) => o is Color color && Equals(color);

		public bool Equals(Color color) =>
			color.A == A
			&& color.R == R
			&& color.G == G
			&& color.B == B;

		public override int GetHashCode() => (A << 8) ^ (R << 6) ^ (G << 4) ^ B;

		public override string ToString() => $"[Color: {A:X8};{R:X8};{G:X8};{B:X8}]";

		public string ToString(string format, IFormatProvider provider) => ToString();

		public static bool operator ==(Color color1, Color color2) => color1.Equals(color2);

		public static bool operator !=(Color color1, Color color2) => !color1.Equals(color2);

		internal Color WithOpacity(double opacity) => new Color((byte)(A * opacity), R, G, B);
	}
}