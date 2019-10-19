using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proximus
{
    internal class Logger
    {
        readonly IEnumerable<ILoggerSink> sinks;
        readonly ILoggerSink sink;
        Action<string> runtimeLogger;

        internal Logger(ILoggerSink sink)
        {
            this.sink = sink;
            runtimeLogger = LogSingle;
        }

        internal Logger(IEnumerable<ILoggerSink> sinks)
        {
            if (sinks == null || sinks.Count() == 0)
            {
                runtimeLogger = (msg) => { };
            }
            if (sinks.Count() == 1)
            {
                this.sink = sinks.First();
                runtimeLogger = LogSingle;
            }
            else
            {
                this.sinks = sinks;
                runtimeLogger = LogMultiple;
            }
        }

        public void Log(string message)
        {
            runtimeLogger(message);
        }

        private void LogMultiple(string message)
        {
            var exHandler = Parallel.ForEach(sinks, (s) =>
            {
                try
                {
                    s.Log(message);
                }
                catch (Exception)
                {
                }
            });

        }

        private void LogSingle(string message)
        {
            sink.Log(message);
        }
    }
}