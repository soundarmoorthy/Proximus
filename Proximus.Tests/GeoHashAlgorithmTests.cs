using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Proximus.Tests
{
    [TestClass]
    public class GeoHashAlgorithmTests
    {

        [TestMethod]
        public void Computes_Only_GeoHashes_With_Length_7()
        {
            using (WorkflowDatastore store = TestSetup.Store())
            {
                GeohashAlgorithm a = new GeohashAlgorithm(store, x => { });
                var code = "23fghn9";
                a.ComputeAndStore(code);
                Assert.AreEqual(code, store.Geocodes().First().Code);
                Assert.AreEqual(store.Geocodes().Count(), 1);
            }
        }

        [TestMethod]
        public void Computes_All_Valid_Suffixes()
        {
            using (WorkflowDatastore store = TestSetup.Store())
            {
                GeohashAlgorithm a = new GeohashAlgorithm(store, x => { });
                var code = "010123";
                a.ComputeAndStore(code);

                var actuals = store.Geocodes().ToList();
                Assert.AreEqual(actuals.Count(), GeohashAlgorithm.base32().Count());
                foreach (var actual in actuals)
                {
                    Assert.IsNotNull(actual);
                    Assert.IsNotNull(actual.Code);
                    Assert.AreEqual(actual.Code.Length, 7);
                    //If we don't use distinct the IsSubsetOf method tries to 
                    //match the duplicates in the superset as well. Our goal is 
                    //to make sure all generated geohashes has character set as
                    //defined by the algorithm, so this should still test the
                    //required functionality.
                    CollectionAssert.IsSubsetOf(actual.Code.Distinct().ToArray(),
                        GeohashAlgorithm.base32().ToArray());
                }
            }
        }

        [TestMethod]
        public void Geohash_Suffixes_Proper_Subset_Returns_False_For_Disallowed_Chars()
        {
            var subset = new Geocode { Code = "ialoiao" };

            var valid = GeohashAlgorithm.Valid(subset);

            Assert.IsFalse(valid);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throws_Exception_On_Compute_For_Inalid_Chars()
        {
            using (var store = TestSetup.Store())
            {
                var a = new GeohashAlgorithm(store, x => { });
                a.ComputeAndStore("lila");
            }
        }
    }
}
