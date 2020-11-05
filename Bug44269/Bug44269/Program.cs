var asm = System.Reflection.Assembly.Load(typeof(Test).Assembly.GetName().Name);
System.Console.WriteLine(asm);

record Test();