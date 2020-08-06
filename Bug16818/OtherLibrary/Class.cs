using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug16818
{
    public class LoggerFactory
    {
        private IDisposable[] _providers;

        private readonly object _sync;

        public LoggerFactory()
        {
            _sync = new object();
        }

        public static void Test()
        {
            // var r = Array.Empty<IDisposable>();
            var r = new int[0];
        }
    }
}
