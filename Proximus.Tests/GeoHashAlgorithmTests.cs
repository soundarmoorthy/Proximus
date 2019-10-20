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
                foreach (var actual in actuals)
                {
                    Assert.IsNotNull(actual);
                    Assert.IsNotNull(actual.Code);
                    Assert.AreEqual(actual.Code.Length, 7);
                    CollectionAssert.IsSubsetOf(actual.Code.ToCharArray(),
                        GeohashAlgorithm.suffix().ToArray());
                }
            }
        }
    }
}
