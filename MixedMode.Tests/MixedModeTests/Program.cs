using MyLibrary;
using System;
using System.Threading;
using Uno.Extensions;
using Windows.Foundation.Collections;

namespace WasmTimerTests
{
    public class Class1
    {
        static event VectorChangedEventHandler<object> VectorChanged;

        static void Main(string[] args)
        {
            VectorChangedEventHandler<object> onVectorChanged =
                (o, e) => OnItemsSourceSingleCollectionChanged(o, e.ToNotifyCollectionChangedEventArgs());

            VectorChanged += onVectorChanged;

            VectorChanged?.Invoke(null, new MyArgs());
        }

        private static void OnItemsSourceSingleCollectionChanged(object o, object p)
        {
            Console.WriteLine("OnItemsSourceSingleCollectionChanged");
        }

        private class MyArgs : IVectorChangedEventArgs
        {
            public CollectionChange CollectionChange => CollectionChange.ItemChanged;

            public uint Index => 0;
        }
    }
}
