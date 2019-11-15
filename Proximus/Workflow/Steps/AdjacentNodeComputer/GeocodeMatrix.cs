using System;
using System.Linq;
using System.Collections.Generic;
using LiteDB;

namespace Proximus
{
    internal class GeocodeMatrix : IEntity
    {
        public Geocode Geocode { get;  set; }
        public  Geocode[] neighbours { get; set; }

        [BsonField("Id")]
        public string Id
        {
            get { return this.Geocode.Code; }
            set { value.FirstOrDefault(); }//Make compiler happy  
        }

        public GeocodeMatrix()
        {

        }

        private static readonly int neighbourCount = Enum.GetValues(typeof(Direction)).GetLength(0);

        public GeocodeMatrix(string geoHash, Geocode[] geocodes)
        {
            this.Geocode = new Geocode() { Code = geoHash };

            if (geocodes == null)
            {
                neighbours = Enumerable.Repeat(Geocode.None, neighbourCount).ToArray();
            }
            else
                neighbours = geocodes;
        }


        public static GeocodeMatrix Create(string geohash, Geocode[] geocodes = null) =>
            new GeocodeMatrix(geohash, geocodes);
      
        public IEnumerable<Geocode> Neighbours() => neighbours;
            

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