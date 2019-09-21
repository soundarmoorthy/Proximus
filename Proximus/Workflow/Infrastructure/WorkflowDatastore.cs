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
        public WorkflowDatastore(string dir)
        {
            InitializeStore(dir);

        }
        private void InitializeStore(string dir)
        {
            codes = new DBCollection<Geocode>(dir);
            distances = new DBCollection<GeoDistance>(dir);
            matrices = new DBCollection<GeocodeMatrix>(dir);
        }


        public void Add(Geocode code) => codes.Add(code);

        public void Add(GeoDistance distance) => distances.Add(distance);

        public void Add(GeocodeMatrix matrix) => matrices.Add(matrix);

        public IEnumerable<GeoDistance> GeoDistances() => distances.enumerate();

        public IEnumerable<GeocodeMatrix> GeocodeMatrices() => matrices.enumerate();

        public IEnumerable<Geocode> Geocodes() => codes.enumerate();

        internal bool Exists(Geocode geoCode) => codes.Exists(geoCode);

        internal bool Exists(GeocodeMatrix matrix) => matrices.Exists(matrix);
    }

    public class DBCollection<T> where T : IEntity
    {
        LiteDatabase database;
        LiteCollection<T> collection;

        public DBCollection(string dir)
        {
            database = new LiteDatabase(Path.Combine(dir, $"{nameof(T)}.db"));
            collection = database.GetCollection<T>();
        }

        public void Add(T data)
        {
            collection.Upsert(data);
        }

        public IEnumerable<T> enumerate() => collection.Query().ToEnumerable();

        public bool Exists(T data) => false;
    }
}
