using System;
using System.Diagnostics;
using System.Drawing.Text;
using System.Globalization;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

// Console.WriteLine("Start");

// const int outerCount = 1;
const int outerCount = 10000;

Test.MyClass m = new();

// Nullable invocation
var r1 = Stopwatch.StartNew();
for (int i = 0; i < outerCount; i++)
{
	m.Content = i;
}
r1.Stop();

//// Int32 invocation
//var r2 = Stopwatch.StartNew();
//for (int i = 0; i < outerCount; i++)
//{
//	Invoke2(t2, disposable);
//}
//r2.Stop();

Console.WriteLine($"r1={r1.Elapsed}");

namespace Test
{
	public partial class MyClass : DependencyObject
	{
		static int _value;

		public object Content
		{
			get { return (object)GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}

		// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ContentProperty =
			DependencyProperty.Register(
				"Content",
				typeof(object),
				typeof(MyClass), 
				new PropertyMetadata(
					null,
				 propertyChangedCallback: (s, e) => (s as MyClass).OnContentChanged(e.OldValue, e.NewValue)
				 propertyChangedCallback: (s, e) => ((MyClass)s).OnContentChanged(e.OldValue, e.NewValue)
				// propertyChangedCallback: OnStaticContentChanged
				// propertyChangedCallback: (s, e) => OnStaticContentChanged(s, e)
				)
			);

		static void OnStaticContentChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var end = (int)e.NewValue * 100;
			for (int i = 0; i < end; i++)
			{
				_value = i;
			}
		}

		void OnContentChanged(object l, object r)
		{
			var end = (int)r * 100;
			for (int i = 0; i < end; i++)
			{
				_value = i;
			}
		}
	}
}
