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
        private const string MemoryStreamDB = null;
        [TestMethod]
        public void Test_GeoHashAlgorithm_Computes_Only_GeoHashes_With_Length_7()
        {
            using (WorkflowDatastore store = new WorkflowDatastore(MemoryStreamDB))
            {
                GeohashAlgorithm a = new GeohashAlgorithm(store, x => { });
                var code = "23fghn9";
                a.Compute(code);
                Assert.AreEqual(code, store.Geocodes().First().Code);
                Assert.AreEqual(store.Geocodes().Count(), 1);
            }
        }

        [TestMethod]
        public void Test_GeoHashAlgorithm_Computes_All_Valid_Suffixes()
        {
            using (WorkflowDatastore store = new WorkflowDatastore(MemoryStreamDB))
            {
                GeohashAlgorithm a = new GeohashAlgorithm(store, x => { });
                var code = "010123";
                a.Compute(code);

                var actuals = store.Geocodes().ToList();
                Assert.AreEqual(actuals.Count(), 32);
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
                        GeohashAlgorithm.suffix().ToArray());
                }
            }
        }
    }
}
