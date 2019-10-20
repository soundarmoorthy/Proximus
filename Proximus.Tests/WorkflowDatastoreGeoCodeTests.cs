using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Reflection;

namespace Proximus.Tests
{
    [TestClass]
    public class WorkflowDatastoreGeoCodeTests
    {
        private const string MemoryStreamDB = null;

        [TestMethod]
        public void WorkflowDataStore_Add_GeoCode_Succeeds()
        {
            //setup
            using (var store = new WorkflowDatastore(MemoryStreamDB))
            {
                store.Add(Geocode.None);

                //Assert. Since this is a clean DB, we need to make sure
                //that the data we inserted is the one we read, and not 
                //anything else, which is stored already.
                var geocodes = store.Geocodes();
                Assert.AreEqual(geocodes.Count(), 1);

                var first = geocodes.First();
                Assert.AreEqual(Geocode.None, first);
                Assert.AreEqual(Geocode.None.Code, first.Code);
            }
        }

        [TestMethod]
        public void WorkflowDataStore_Exists_GeoCode_Succeeds()
        {
            using (var store = new WorkflowDatastore(MemoryStreamDB))
            {
                store.Add(Geocode.None);
                var exists = store.Exists(Geocode.None);
                Assert.IsTrue(exists);

                exists = store.Exists(new Geocode() { Code = "KiKiMuMu" });
                Assert.IsFalse(exists);
            }
        }

        [TestMethod]
        public void WorkflowDataStore_Enumerate_GeoCode_Succeeds()
        {
            using (var store = new WorkflowDatastore(MemoryStreamDB))
            {
                int count = Populate(store);
                var geocodes = store.Geocodes();
                Assert.IsNotNull(geocodes);
                Assert.AreEqual(geocodes.Count(), count);
                Assert.IsTrue(geocodes.All(x => x != null && x.Code != null));
            }
        }
        private int Populate(WorkflowDatastore store)
        {
            store.Add(new Geocode() { Code = "Soundar" });
            store.Add(new Geocode() { Code = "Sandeep" });
            store.Add(new Geocode() { Code = "AkshayD" });
            store.Add(new Geocode() { Code = "Aravind" });
            return 4;
        }
    }
}
