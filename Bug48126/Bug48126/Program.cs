using System;
using System.Diagnostics;
using System.Drawing.Text;
using System.Globalization;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

// Console.WriteLine("Start");

const int outerCount = 1000000;
int j = 0;

// Nullable invocation
var r1 = Stopwatch.StartNew();
for (int i = 0; i < outerCount; i++)
{
	var r = GetMyNullable();

	if(r != null)
	{
		j++;
	}
}
r1.Stop();

// Int32 invocation
var r2 = Stopwatch.StartNew();
for (int i = 0; i < outerCount; i++)
{
	var r = GetMyNonNullable();

	if(r != 0)
	{
		j++;
	}
}
r1.Stop();

Console.WriteLine($"r1={r1.Elapsed} r2={r2.Elapsed}");

int GetMyNonNullable()
{
	for (int i = 0; i < 1000000; i++)
	{
	}

	return 0;
}

int? GetMyNullable()
{
	for (int i = 0; i < 1000000; i++)
	{
	}

	return null;
}
