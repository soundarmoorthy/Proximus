using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Proximus.Tests
{
    [TestClass]
    public class LoggerTests
    {
        const string hi = "hi";

        [TestMethod]
        public void ConsoleLoggerSink_Is_Singleton()
        {
            Assert.ReferenceEquals(ConsoleLoggerSink.Instance,
                ConsoleLoggerSink.Instance);
        }


        /// <summary>
        /// Verifies if all the registered log sinks are called when Logger.Log is called
        /// </summary>
        [TestMethod]
        public void Test_All_Registered_Logger_Sinks_Are_Called()
        {
            var loggerSinks = GetLoggerSinks();
            Logger logger = new Logger(loggerSinks.Select(x => x.Object));
            logger.Log(hi);

            
            foreach (var sink in loggerSinks)
            {
                sink.Verify(x => x.Log(hi), Times.Exactly(1));
            }
        }

        /// <summary>
        /// Verifies if single registered log sink is called correctly 
        /// when Logger.Log is called
        /// </summary>
        [TestMethod]
        public void Test_Single_Registered_Logger_Sink_Is_Called()
        {
            var sink = Create();
            Logger logger = new Logger(sink.Object);
            logger.Log(hi);
            sink.Verify(x => x.Log(hi), Times.Exactly(1));
        }

        private IEnumerable<Mock<ILoggerSink>> GetLoggerSinks() 
            => new[] { Create(), Create(), CreateWithException() };

        private Mock<ILoggerSink> Create()
        {
            var mock = new Mock<ILoggerSink>();
            mock.Setup(x => x.Log(hi)).Verifiable();
            return mock;
        }

        private Mock<ILoggerSink> CreateWithException()
        {
            var mock = new Mock<ILoggerSink>();
            mock.Setup(x => x.Log(hi)).Throws(new InvalidOperationException());
            return mock;
        }
    }
}
