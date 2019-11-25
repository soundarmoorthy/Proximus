using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Proximus.Tests
{
    [TestClass]
    public class AdjecencyNodeProcessorTests
    {

        [TestMethod]
        public void Invalid_Geocodes_Are_Not_Accepted_For_Processing()
        {
            using (var store = TestSetup.Store())
            {
                //Add invalid Geocode
                store.Add(Geocode.None);

                var state = TestSetup.Workflowstate(store);
                var pro = new AdjecencyNodeProcessor(state);
                pro.Start();

                Assert.IsFalse(store.GeocodeMatrices().Any());
            }
        }

        [TestMethod]
        public void Valid_Geocode_Is_Processed_Succesfully_And_The_Neighbours_Are_Intact()
        {
            using (var store = TestSetup.Store())
            {
                //Add invalid Geocode
                store.Add(new Geocode() { Code = "9239391" });

                var state = TestSetup.Workflowstate(store);
                var pro = new AdjecencyNodeProcessor(state);

                pro.Start();

                var matrices = store.GeocodeMatrices();

                Assert.AreEqual(1, matrices.Count());

                var matrix = matrices.First();
                Assert.IsNotNull(matrix);

                CollectionAssert.AllItemsAreNotNull(matrix.Neighbours().ToArray());
                Assert.IsTrue(matrix.Neighbours().All(x => GeohashAlgorithm.Valid(x)));
            }
        }


    }
}
