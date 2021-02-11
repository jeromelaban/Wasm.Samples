using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

static class Program
{
	// const int outerCount = 1;
	const int outerCount = 50000;
	const int innerCount = 10000;

	static void Main(string[] args)
	{
		MethodCallBenchmark();
		DictionaryRefRefBenchmark();
		DictionaryIntIntBenchmark();
	}

	private static void DictionaryRefRefBenchmark()
	{
		const int itemCount = 10000;

		var values = new Dictionary<string, object>();

		for (int i = 0; i < itemCount; i++)
		{
			values.Add(i.ToString(), i);
		}

		// Warmup
		Bench(itemCount, values);

		var r1 = Stopwatch.StartNew();
		Bench(itemCount, values);
		r1.Stop();

		Console.WriteLine($"r1={r1.Elapsed}");

		static void Bench(int itemCount, Dictionary<string, object> values)
		{
			int count = 0;
			for (int j = 0; j < 10; j++)
			{
				for (int i = 0; i < itemCount; i++)
				{
					if (values.TryGetValue("", out var res))
					{
						// count += (int)res;
					}
				}
			}
		}
	}

	private static void DictionaryIntIntBenchmark()
	{
		const int itemCount = 10000;

		var values = new Dictionary<int, int>();

		for (int i = 0; i < itemCount; i++)
		{
			values.Add(i, i);
		}

		// Warmup
		Bench(itemCount, values);

		var r1 = Stopwatch.StartNew();
		Bench(itemCount, values);
		r1.Stop();

		Console.WriteLine($"r1={r1.Elapsed}");

		static void Bench(int itemCount, Dictionary<int, int> values)
		{
			int count = 0;
			for (int j = 0; j < 10; j++)
			{
				for (int i = 0; i < itemCount; i++)
				{
					if (values.TryGetValue(i, out var res))
					{
						// count += (int)res;
					}
				}
			}
		}
	}

	private static void MethodCallBenchmark()
	{
		// Generic invocation
		var r1 = Stopwatch.StartNew();
		int count1 = 0;
		for (int i = 0; i < outerCount; i++)
		{
			count1 += TestGeneric<int>(i, 42);
		}
		r1.Stop();

		// Normal invocation
		var r2 = Stopwatch.StartNew();
		int count2 = 0;
		for (int i = 0; i < outerCount; i++)
		{
			count2 += TestNormal(i, 42);
		}
		r2.Stop();

		Console.WriteLine($"r1={r1.Elapsed} r2={r2.Elapsed} {count1 + count2}");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	static int TestGeneric<T>(T value, int other)
	{
		for (int i = 0; i < innerCount; i++)
		{
			other += i;
		}

		return other;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	static int TestNormal(int value, int other)
	{
		for (int i = 0; i < innerCount; i++)
		{
			other += i;
		}

		return other;
	}
}

