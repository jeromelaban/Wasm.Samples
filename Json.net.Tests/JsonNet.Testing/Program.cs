using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonNet.Testing
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var test = JsonConvert.DeserializeObject<MyTest>("{ 'myValue': 'test' }");

			Console.WriteLine($"MyValue = {test.MyValue2}");
		}
	}

	public class MyTest
	{
		public MyTest()
		{

		}

		[JsonConstructor]
		public MyTest(string myValue)
		{
			MyValue2 = myValue;
		}

		public string MyValue2 { get; set; }
	}
}
