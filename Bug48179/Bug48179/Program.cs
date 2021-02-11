using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

static class Program
{
	// const int outerCount = 1;
	const int innerCount = 1;
	const int maxDepth = 4;

	static void Main(string[] args)
	{
		NestedLoopPerf_ForLoop();
		NestedLoopPerf_ForEachLoop();
	}

	private static void NestedLoopPerf_ForLoop()
	{
		var list = Enumerable.Range(0, 10).ToList();

		var r1 = Stopwatch.StartNew();
		Bench(list);
		r1.Stop();

		Console.WriteLine($"r1={r1.Elapsed}");

		static void Bench(List<int> array)
		{
			for (int i = 0; i < innerCount; i++)
			{
				Nest(0);
			}

			void Nest(int depth)
			{
				if(depth == maxDepth)
				{
					return;
				}

				for (int i = 0; i < array.Count; i++)
				{
					Nest(depth + 1);
				}
			}
		}
	}

	private static void NestedLoopPerf_ForEachLoop()
	{
		var list = Enumerable.Range(0, 10).ToList();

		var r1 = Stopwatch.StartNew();
		Bench(list);
		r1.Stop();

		Console.WriteLine($"r1={r1.Elapsed}");

		static void Bench(List<int> list)
		{
			for (int i = 0; i < innerCount; i++)
			{
				Nest(0);
			}

			void Nest(int depth)
			{
				if(depth == maxDepth)
				{
					return;
				}

				foreach(var item in list)
				{
					Nest(depth + 1);
				}
			}
		}
	}
}

