using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.IO;
using Proximus;

namespace Proximus.Tests
{
    [TestClass]
    public class DBCollectionTests
    {
        private static readonly string MemoryStreamDB = null;
        public DBCollectionTests()
        {

        }


       [TestMethod]
        public void LiteDatabase_Is_Created_Succesfully()
        {
            var path = Path.Combine(Path.GetTempPath());
            using (var duck = new DBCollection<Foo>(path))
            {
                duck.Add(new Foo("100"));
                duck.Exists(new Foo("100"));
                duck.enumerate();
            }

            var expected = Path.Combine(Path.GetTempPath(), $"{typeof(Foo).FullName}.db");
            Assert.IsTrue(File.Exists(expected));
            File.Delete(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void MemoryStream_Disposed_Properly()
        {
            DBCollection<Foo> foo;
            using(foo = new DBCollection<Foo>(MemoryStreamDB))
            {

            }
            foo.Add(new Foo("00"));         
        }

        [TestMethod]
        public void Add_Method_Updates_Existing_Entries_if_present()
        {
            using(var db = new DBCollection<Foo>(MemoryStreamDB))
            {
                db.Add(new Foo("100"));
                db.Add(new Foo("100"));

                Assert.AreEqual(db.enumerate().Count(), 1);
            }
        }

        [TestMethod]
        public void Add_Method_Adds_New_Entries_If_Not_Present()
        {
            using(var db = new DBCollection<Foo>(MemoryStreamDB))
            {
                db.Add(new Foo("100"));
                db.Add(new Foo("200"));
                Assert.AreEqual(db.enumerate().Count(), 2);
            }
        }

        [TestMethod]
        public void Enumerate_Doest_Fail_When_No_Element_In_Collection()
        {
            using (var db = new DBCollection<Foo>(MemoryStreamDB))
            {
                Assert.IsNotNull(db.enumerate());
                Assert.AreEqual(db.enumerate().Count(), 0);
            }
        }
    }

    public class Foo : IEntity
    {
        public Foo()
        {

        }
        public Foo(string id)
        {
            this.Id = id;
        }

        public string Id { get; private set; }

    }
}
