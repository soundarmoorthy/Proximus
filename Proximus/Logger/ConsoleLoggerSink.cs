using System;
namespace Proximus
{
    public class ConsoleLoggerSink : ILoggerSink
    {
        public ConsoleLoggerSink()
        {

        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
