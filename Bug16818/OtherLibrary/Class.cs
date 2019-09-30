using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug16818
{
    public class LoggerFactory
    {
        private IDisposable[] _providers = (IDisposable[])new IDisposable[0];

        private readonly object _sync = new object();
    }
}
