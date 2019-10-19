using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using LiteDB;

namespace Proximus
{
    public class WorkflowDatastore
    {
        private  DBCollection<Geocode> codes;
        private  DBCollection<GeoDistance> distances;
        private  DBCollection<GeocodeMatrix> matrices;
        public WorkflowDatastore(string dir, bool drop=false)
        {
            InitializeStore(dir, drop);

        }

        private void InitializeStore(string dir, bool drop)
        {
            codes  =  new DBCollection<Geocode>(dir, drop);
            distances = new DBCollection<GeoDistance>(dir,drop);
            matrices = new DBCollection<GeocodeMatrix>(dir,drop);
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

    }

    public class DBCollection<T> where T : IEntity
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
        public DBCollection(string dir, bool drop=false)
        {
            database = new LiteDatabase(Path.Combine(dir, $"{typeof(T).FullName}.db"));
            if (drop)
                database.DropCollection(typeof(T).Name);

            collection = database.GetCollection<T>(typeof(T).Name);
        }

        public void Add(T data)
        {
            collection.Upsert(data);
        }

        public bool Exists(T data)
        {
            return collection.Find(x => x.Equals(data)).Any();
        }

        public IEnumerable<T> enumerate() => collection.Query().ToEnumerable();
    }
}
