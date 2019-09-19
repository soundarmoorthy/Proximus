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

        public void LogMultiple(string message)
        {
            Parallel.ForEach(sinks, (sink) => sink.Log(message));
        }

        public void LogSingle(string message)
        {
            sinks.First().Log(message);
        }
    }
}