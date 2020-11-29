using System;

Test.Run();

public class Test
{
	delegate void MyTargetHandler(object sender, MyTest args);

	public static void Run()
	{
		MyTargetHandler handler = MyTarget;

		var r = handler.DynamicInvoke(null, new MyTest());

		Console.WriteLine($"Result: {r}");
	}

	public static void MyTarget(object sender, MyTest args)
	{
		Console.WriteLine("test");
	}
}

public class MyTest : EventArgs
{
}