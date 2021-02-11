using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

static class Program
{
	// const int outerCount = 1;
	const int outerCount = 50000;
	const int innerCount = 10000;

	static void Main(string[] args)
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

