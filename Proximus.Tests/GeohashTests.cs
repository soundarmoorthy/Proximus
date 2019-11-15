using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Proximus.Tests
{
    [TestClass]
    public class GeohashTests
    {
        public GeohashTests()
        {
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetBounds_Fails_with_Exception_On_Invalid_Geohash()
        {
            Geohash hash = new Geohash();

            hash.GetBound("^&&&(&(&*");
        }

        [TestMethod]
        public void GetBounds_Returns_Proper_Values_On_Valid_Geohash()
        {
            Geohash hash = new Geohash();

            var actual  = hash.GetBound("11uskk");

            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.NE.Lat);
            Assert.IsNotNull(actual.NE.Lng);


            Assert.IsNotNull(actual.SW.Lat);
            Assert.IsNotNull(actual.SW.Lng);
        }
    }
}
