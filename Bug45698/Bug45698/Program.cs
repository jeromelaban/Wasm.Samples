using System;
using System.Resources;

var r = new ResourceManager("Bug44269.Resources.Strings", typeof(Bug44269.Resources.Strings).Assembly);
Console.WriteLine(r.GetString("MyDefaultString"));
