using System;
using System.Threading;
using System.Threading.Tasks;

namespace WasmThreading
{
	public class Program
	{
		static void Main(string[] args)
		{
			Run();
		}

		private static async Task Run()
		{
			Console.WriteLine("Startup");

			var evt = new ManualResetEvent(false);
			var tcs = new TaskCompletionSource<bool>();

			var t = new Thread(() =>
			{

				LogMessage($"Thread begin");

				tcs.SetResult(true);

				LogMessage($"Waiting for event");

				evt.WaitOne();

				LogMessage($"Got event, terminating thread");
			});

			t.Start();

			LogMessage($"Waiting for completion source");

			await tcs.Task;

			LogMessage($"Got task result, raising event");

			evt.Set();

			LogMessage($"Main thread exiting");
		}

		private static void LogMessage(string message)
			=> Console.WriteLine($"[tid:{Thread.CurrentThread.ManagedThreadId}] {message}");
	}
}
