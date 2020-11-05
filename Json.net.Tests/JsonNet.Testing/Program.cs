using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

var test = JsonConvert.DeserializeObject<MyTest>("{ 'myValue': 'test' }");

Console.WriteLine($"MyValue = {test.MyValue2}");

//var a = Assembly.Load("JsonNet.Testing");
//Console.WriteLine($"{a}");

var r = new List<IDisposable>(0);

var t = new object();
var _originalObjectRef = new WeakReference(t);
string name = "gsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgd2";
var hashCode = _originalObjectRef.Target?.GetHashCode();
var propertyName = "gsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgd2";
object newValue = "gsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgdgsdfgsdfgdfsgdsfgd2"; ;
object value = null;
object previousValue = null;
StringComparison precedence = StringComparison.CurrentCulture;
StringComparison previousPrecedence = StringComparison.CurrentCulture;
StringComparison newPrecedence = StringComparison.CurrentCulture;

try
{
	var message = $"SetValue on [{name}/{hashCode:X8}] " +
		$"for [{propertyName}] to [{newValue}] (req:{value} reqp:{precedence} p:{previousValue} pp:{previousPrecedence} np:{newPrecedence})";

	Console.WriteLine(message);
}
finally
{
	Console.WriteLine("test");
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
