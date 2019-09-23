using System;
using System.Linq;
using System.Collections.Generic;

namespace Proximus
{
    public class GeocodeMatrix : IEntity
    {
        public Geocode GeoCode { get; set; }
        public Geocode[] neighbours { get; set; }

        public GeocodeMatrix()
        {

        }

        public GeocodeMatrix(string geoHash)
        {
            this.GeoCode = new Geocode() { Code = geoHash };

            var count = Enum.GetValues(typeof(Direction)).GetLength(0);
            neighbours = new Geocode[count];
        }


        public static GeocodeMatrix Create(string geohash) => new GeocodeMatrix(geohash);
      
        public IEnumerable<Geocode> Neighbours() => neighbours;
            
        private int index=0;
        public GeocodeMatrix Add(string c)
        {
            neighbours[index++] = new Geocode() { Code = c };
            return this;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;

            return this.GeoCode == ((GeocodeMatrix)obj).GeoCode;
        }

        public override string ToString()
        {
            return $"{this.GeoCode}-[{string.Join(',', neighbours.Select(x => x.Code))}]";
        }

    }
}
