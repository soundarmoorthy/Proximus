using System;
using System.Linq;
using System.Collections.Generic;
using LiteDB;

namespace Proximus
{
    internal class GeocodeMatrix : IEntity
    {
        public Geocode Geocode { get; set; }
        public Geocode[] neighbours { get; set; }

        [BsonField("Id")]
        public string Id
        {
            get { return this.Geocode.Code; }
            set { }
        }

        public GeocodeMatrix()
        {

        }

        public GeocodeMatrix(string geoHash)
        {
            this.Geocode = new Geocode() { Code = geoHash };

            var count = Enum.GetValues(typeof(Direction)).GetLength(0);
            neighbours = new Geocode[count];
            for(int i =0;i<neighbours.Count();i++)
               neighbours[i] = Geocode.None;
        }


        public static GeocodeMatrix Create(string geohash) => new GeocodeMatrix(geohash);
      
        public IEnumerable<Geocode> Neighbours() => neighbours;
            
        private int index=0;
        internal GeocodeMatrix Add(string c)
        {
            neighbours[index++] = new Geocode() { Code = c };
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is GeocodeMatrix))
                return false;

            return this.Geocode.Equals(((GeocodeMatrix)obj).Geocode);
        }

        public override int GetHashCode()
        {
            return this.Geocode.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Geocode}-[{string.Join(',', neighbours.Select(x => x.Code))}]";
        }
    }
}
