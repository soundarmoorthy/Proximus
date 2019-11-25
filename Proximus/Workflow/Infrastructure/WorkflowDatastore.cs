using System;
using System.Collections.Generic;

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
}
