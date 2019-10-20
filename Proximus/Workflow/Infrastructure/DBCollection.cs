using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;

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
        /// <param name="drop">Delete existing collection, if this is True. But in production we 
        /// won't set this to true. This is primarily used during 
        /// testing to cleanup the files and use freash ones for each run</param>
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
            collection.Upsert(data);
        }

        public bool Exists(T data)
        {
            //All IEntity implementations will have a BsonField with Id
            return collection.Exists(x => x.Id == data.Id);
        }

        public IEnumerable<T> enumerate() => collection.Query().ToEnumerable();

        bool disposed = true;
        public void Dispose()
        {
            if(!disposed)
            {
                if(database !=null) database.Dispose();
                disposed = true;
            }
        }
    }
}
