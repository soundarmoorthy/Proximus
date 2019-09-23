using System;
namespace Proximus
{
    public class ConsoleLoggerSink : ILoggerSink
    {
        private ConsoleLoggerSink()
        {

        }

        private static ConsoleLoggerSink sink;
        public static ConsoleLoggerSink Instance
        {
            get
            {
                if (sink == null)
                    sink = new ConsoleLoggerSink();
                return sink;
            }
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
