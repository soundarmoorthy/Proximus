using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Proximus.Tests
{
    [TestClass]
    public class WorkflowDatastoreTests
    {
        [TestMethod]
        public void WorkflowDataStore_Add_GeoCode_Succeeds()
        {
            //setup
            var store = new WorkflowDatastore(".", drop:true);

            //CUT
            store.Add(Geocode.None);

            //Assert. Since this is a clean DB, we need to make sure
            //that the data we inserted is the one we read, and not 
            //anything else, which is stored already.
            var geocodes = store.Geocodes();
            Assert.AreEqual(geocodes.Count(), 1);
            Assert.AreEqual(geocodes.First(), Geocode.None);
            Assert.AreEqual(geocodes.First().Code, Geocode.NoneCode);
        }


        [TestMethod]
        public void WorkflowDataStore_Exists_GeoCode_Succeeds()
        {
            //setup
            var store = new WorkflowDatastore(".", drop:true);

            //CUT
            store.Add(Geocode.None);

            //Assert
            Assert.IsTrue(store.Exists(Geocode.None));
        }

        [TestMethod]
        public void WorkflowDataStore_Enumerate_GeoCode_Succeeds()
        {
            //setup
            var store = new WorkflowDatastore(".", drop:true);

            //CUT
            store.Add(new Geocode() { Code = "Soundar" });
            store.Add(new Geocode() { Code = "Sandeep" });
            store.Add(new Geocode() { Code = "AkshayD" });
            store.Add(new Geocode() { Code = "Aravind" });

            //Assert
            var geocodes = store.Geocodes();
            Assert.IsNotNull(geocodes);
            Assert.AreEqual(geocodes.Count(), 4);
            Assert.IsTrue(geocodes.All(x => x != null && x.Code != null));
        }
    }
}
