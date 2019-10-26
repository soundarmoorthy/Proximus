using LiteDB;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Proximus
{
    public class DBCollection<T> : IDisposable
        where T : IEntity
    {
        LiteDatabase database;
        LiteCollection<T> collection;

        /// <summary>
        /// Initialize the DBCollection
        /// </summary>
        /// <param name="dir">Directory in which the collection should be created. Read and Write permissions 
        /// should exist</param>
        internal DBCollection(string dir)
        {
            if (dir == null)
                database = new LiteDatabase(new MemoryStream());
            else
                database = new LiteDatabase(Path.Combine(dir, $"{typeof(T).FullName}.db"));

            collection = database.GetCollection<T>(typeof(T).Name);
        }

        public void Add(T data)
        {
            if (this.Exists(data))
                collection.Update(data);
            else
                collection.Insert(data);
        }

        public bool Exists(T data)
        {
            var result = collection.Query().ToEnumerable();

            //All IEntity implementations will have a BsonField with Id
            return result.Any(x => x.Id == data.Id);
        }

        public IEnumerable<T> enumerate() => collection.Query().ToEnumerable();

        bool disposed;
        public void Dispose()
        {
            if(!disposed)
            {
                if (database != null)
                {
                    database.Dispose();
                    database = null;
                    collection = null;
                }
                disposed = true;
            }
        }
    }
}
