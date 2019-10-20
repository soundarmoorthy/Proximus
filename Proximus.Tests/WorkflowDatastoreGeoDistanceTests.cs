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
    public class WorkflowDatastoreGeoDistanceTests

    {
        private const string MemoryStreamDB = null;

        [TestMethod]
        public void WorkflowDataStore_Add_GeoDistance_Succeeds()
        {
            //setup
            using (var store = new WorkflowDatastore(MemoryStreamDB))
            {
                var expected = GeoDistance.Create("Ottiyambakkam", "Guindy", 10.0);
                store.Add(expected);

                //Assert. Since this is a clean DB, we need to make sure
                //that the data we inserted is the one we read, and not 
                //anything else, which is stored already.
                var distances = store.GeoDistances();
                Assert.AreEqual(distances.Count(), 1);
                var actual = distances.First();
                Assert.AreEqual(expected, actual);
                Assert.AreEqual(expected.Start, actual.Start);
                Assert.AreEqual(expected.End, actual.End);
                Assert.AreEqual(expected.Miles, actual.Miles);
                Assert.AreNotSame(expected, actual);
            }
        }

        [TestMethod]
        public void WorkflowDataStore_Exists_GeoDistance_Succeeds()
        {
            using (var store = new WorkflowDatastore(MemoryStreamDB))
            {
                var expected = GeoDistance.Create("foo", "bar", 10.0);
                store.Add(expected);
                var exists = store.Exists(expected);
                Assert.IsTrue(exists);

                expected = GeoDistance.Create("duck", "bar", 10.0);
                exists = store.Exists(expected);
                Assert.IsFalse(exists);

                expected = GeoDistance.Create("foo", "duck", 10.0);
                exists = store.Exists(expected);
                Assert.IsFalse(exists);

                expected = GeoDistance.Create("bar", "foo", 10.0);
                exists = store.Exists(expected);
                Assert.IsFalse(exists);
            }
        }

        [TestMethod]
        public void WorkflowDataStore_Enumerate_GeoDistance_Succeeds()
        {
            using (var store = new WorkflowDatastore(MemoryStreamDB))
            {
                int count = PopulateDistances(store);
                var geoDistances = store.GeoDistances();
                Assert.IsNotNull(geoDistances);
                Assert.AreEqual(geoDistances.Count(), count);
                Assert.IsTrue(geoDistances.All(x => x != null && 
                x.Start != null && x.End != null && !Double.IsNaN(x.Miles)));
            }
        }

        private int PopulateDistances(WorkflowDatastore store)
        {
            store.Add(GeoDistance.Create("a", "b", 1.54534));
            store.Add(GeoDistance.Create("b", "c", 11.3491));
            store.Add(GeoDistance.Create("c", "d", 111.2342));
            store.Add(GeoDistance.Create("d", "e", 1111.11));
            return 4;
        }
    }
}
