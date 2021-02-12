using System;
using System.Diagnostics;
using System.Drawing.Text;
using System.Globalization;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

// Console.WriteLine("Start");

// const int outerCount = 1;
const int outerCount = 1000000;

var disposable = new MyType();

MyDelegate t1 = Registry.List;
MyDelegate t2 = Function2;

// Nullable invocation
var r1 = Stopwatch.StartNew();
for (int i = 0; i < outerCount; i++)
{
	Invoke1(t1, disposable);
}
r1.Stop();

// Int32 invocation
var r2 = Stopwatch.StartNew();
for (int i = 0; i < outerCount; i++)
{
	Invoke2(t2, disposable);
}
r2.Stop();

Console.WriteLine($"r1={r1.Elapsed} r2={r2.Elapsed}");

void Function2(object sender, EventArgs args)
{
	((IDisposable)sender).Dispose();
	for (int i = 0; i < 100000; i++)
	{
	}
}

void Invoke1(MyDelegate d, IDisposable d2)
{
	d.Invoke(d2, null);
}

void Invoke2(MyDelegate d, IDisposable d2)
{
	d.Invoke(d2, null);
}

public class MyType : IDisposable
{
	static MyType()
	{
		Registry.Register((s, e) => {
			((IDisposable)s).Dispose();
			for (int i = 0; i < 100000; i++)
			{
			}
		});
	}

	public static void Init() { }

	public virtual void Dispose()
	{
	}
}

class Registry
{
	public static MyDelegate List;

	public static void Register(MyDelegate d)
	{
		List += d;
	}
}

delegate void MyDelegate(object sender, EventArgs args);
