using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proximus
{
    public class Logger
    {
        readonly IEnumerable<ILoggerSink> sinks;
        readonly ILoggerSink sink;
        Action<string> runtimeLogger;
        public Logger(IEnumerable<ILoggerSink> sinks)
        {
            if (sinks == null || sinks.Count() == 0)
            {
                runtimeLogger = (msg) => { };
            }

            if (sinks.Count() <= 1)
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
            Parallel.ForEach(sinks, (s) => s.Log(message));
        }

        private void LogSingle(string message)
        {
            sink.Log(message);
        }
    }
}