using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Proximus.Tests
{
    [TestClass]
    public class GeohashNeighboursTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Compute_Throws_Argument_Exception_On_Null()
        {
            GeohashNeighbours.Compute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Compute_Throws_Argument_Exception_On_Empty_Geohashes()
        {
            GeohashNeighbours.Compute(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Compute_Throws_Argument_Exception_On_Null_Empty_Geohashes()
        {
            GeohashNeighbours.Compute("laila");
        }

        [TestMethod]
        public void Compute_Returns_Valid_Matrix_With_Right_Count_Of_Neighbours()
        {
            var matrix = GeohashNeighbours.Compute("bgdc123");

            Assert.IsNotNull(matrix);
            Assert.AreEqual<int>
                (Enum.GetNames(typeof(Direction)).Length,
                matrix.Neighbours().Count());
        }


    }
}
