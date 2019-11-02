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


    }
}
