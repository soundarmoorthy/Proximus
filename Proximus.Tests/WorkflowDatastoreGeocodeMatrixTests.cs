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
    public class WorkflowDatastoreTests
    {

        [TestMethod]
        public void WorkflowDataStore_Add_GeoMatrix_Succeeds()
        {
            //setup
            using (var store = TestSetup.Store())
            {
                var expected = GeocodeMatrix.Create("hello", null);
                store.Add(expected);

                //Assert. Since this is a clean DB, we need to make sure
                //that the data we inserted is the one we read, and not 
                //anything else, which is stored already.
                var matrices = store.GeocodeMatrices();
                Assert.AreEqual(matrices.Count(), 1);

                var actual = matrices.First();
                Assert.AreEqual(expected, actual);

                CollectionAssert.AreEqual(expected.Neighbours().ToList(),
                    actual.Neighbours().ToList());
                Assert.AreNotSame(expected, actual);
            }
        }

        [TestMethod]
        public void WorkflowDataStore_Exists_GeoMatrix_Succeeds()
        {
            using (var store = TestSetup.Store())
            {
                var expected = Create();
                store.Add(expected);
                var exists = store.Exists(expected);
                Assert.IsTrue(exists);

                //The reson why the following constant cannot exist is
                //because the previous call to Create() return a valid
                //random file name and the following test string contains
                //characters that cannot be part of a file name in any OS
                expected = GeocodeMatrix.Create("A::Random**String??<dot>");
                exists = store.Exists(expected);
                Assert.IsFalse(exists);
            }
        }

        [TestMethod]
        public void WorkflowDataStore_Enumerate_GeoMatrix_Succeeds()
        {
            using (var store = TestSetup.Store())
            {
                int count = PopulateMatrices(store);
                var matrices = store.GeocodeMatrices();
                Assert.IsNotNull(matrices);
                Assert.AreEqual(matrices.Count(), count);
                CollectionAssert.AllItemsAreNotNull(matrices.ToList());
            }
        }

        private int PopulateMatrices(WorkflowDatastore store)
        {
            var count = 4;
            for (int i = 0; i < count; i++)
                store.Add(Create());
            return count;
        }

        private static GeocodeMatrix Create()
        {
            //Path.GetTempFileName() gives you random string. Not necessarily
            //unique. But that is good enough
            var values = Enumerable.Repeat(
                new Geocode() { Code = Path.GetRandomFileName() },
                Enum.GetNames(typeof(Direction)).Length).ToArray();
            return GeocodeMatrix.Create(Path.GetRandomFileName(), values);
        }
    }
}
