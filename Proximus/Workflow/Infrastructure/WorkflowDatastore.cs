using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using LiteDB;
using LiteDB.Engine;

namespace Proximus
{
    internal class WorkflowDatastore : IDisposable
    {
        private  DBCollection<Geocode> codes;
        private  DBCollection<GeoDistance> distances;
        private  DBCollection<GeocodeMatrix> matrices;
        public WorkflowDatastore(string dir)
        {
            InitializeStore(dir);

        }

        private void InitializeStore(string dir)
        {
            codes  =  new DBCollection<Geocode>(dir);
            distances = new DBCollection<GeoDistance>(dir);
            matrices = new DBCollection<GeocodeMatrix>(dir);
        }


        public void Add(Geocode code) => codes.Add(code);

        public void Add(GeoDistance distance) => distances.Add(distance);

        public void Add(GeocodeMatrix matrix) => matrices.Add(matrix);

        public IEnumerable<GeoDistance> GeoDistances() => distances.enumerate();

        public IEnumerable<GeocodeMatrix> GeocodeMatrices() => matrices.enumerate();

        public IEnumerable<Geocode> Geocodes() => codes.enumerate();

        public bool Exists(Geocode code) => codes.Exists(code);
        public bool Exists(GeocodeMatrix matrix) => matrices.Exists(matrix);
        public bool Exists(GeoDistance distance) => distances.Exists(distance);

        bool disposed = false;
        public void Dispose()
        {
            if (!disposed)
            {
                if (codes != null) codes.Dispose();
                if (matrices != null) matrices.Dispose();
                if (distances != null) distances.Dispose();
            }
        }
    }

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
