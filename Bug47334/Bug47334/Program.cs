using System;
using System.Diagnostics;
using System.Drawing.Text;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

Console.WriteLine("Start");

Run();

async void Run()
{
	var client = new HttpClient();
	var r = await client.GetAsync("http://invalid.url");

	Console.WriteLine("test");
}