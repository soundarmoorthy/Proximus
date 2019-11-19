using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Proximus.Tests
{
    [TestClass]
    public class HttpRequestExtensionsTests
    {
        public HttpRequestExtensionsTests()
        {
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetTimeout_Throws_ArgumentNullException_On_Null_Request()
        {
            HttpRequestMessage actual = null;
            HttpRequestExtensions.SetTimeout(actual, TimeSpan.FromDays(1));
        }

        [TestMethod]
        public void SetTimeout_Sets_Timeout_Property_Properly_In_Headers()
        {
            var actual = new HttpRequestMessage(HttpMethod.Get, "https://soundararajan.in");
            actual.SetTimeout(TimeSpan.FromSeconds(1));

            Assert.AreEqual(actual.Properties[HttpRequestExtensions.TimeoutPropertyKey], TimeSpan.FromSeconds(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetTimeout_Throws_ArgumentNullException_On_Null_Request()
        {
            HttpRequestMessage actual = null;
            actual.GetTimeout();
        }


        [TestMethod]
        public void GetTimeout_Returns_Null_When_Timeout_Is_Null()
        {
            var actual = new HttpRequestMessage(HttpMethod.Get, "https://soundararajan.in");
            actual.SetTimeout(null);

            Assert.AreEqual(null, actual.GetTimeout());
        }

        [TestMethod]
        public void GetTimeout_Returns_Null_When_Timeout_Is_Not_Set()
        {
            var actual = new HttpRequestMessage(HttpMethod.Get, "https://soundararajan.in");

            Assert.AreEqual(null, actual.GetTimeout());
        }

        [TestMethod]
        public void GetTimeout_Returns_Correct_Value_That_Is_Set()
        {
            var actual = new HttpRequestMessage(HttpMethod.Get, "https://soundararajan.in");
            actual.SetTimeout(TimeSpan.MaxValue);
            Assert.AreEqual(TimeSpan.MaxValue, actual.GetTimeout());
        }

    }
}
