using System;
using System.Linq;
using System.Collections.Generic;

namespace Proximus
{
    public class GeocodeMatrix : IEntity
    {
        public readonly Geocode GeoCode;
        private readonly Dictionary<Direction, Geocode> f;
        private GeocodeMatrix(string geoHash)
        {
            this.GeoCode = new Geocode() {Code = geoHash };
            var kvps = from name in Enum.GetNames(typeof(Direction))
                       let direction = (Direction)Enum.Parse(typeof(Direction), name)
                       select new KeyValuePair<Direction, Geocode>(direction, Geocode.None);

            f = new Dictionary<Direction, Geocode>(kvps);
        }


        public static GeocodeMatrix Create(string geohash) => new GeocodeMatrix(geohash);
      
        public IEnumerable<Geocode> Neighbours() => f.Values;

        public GeocodeMatrix Add(Direction x, string c)
        {
            f[x] = new Geocode() { Code = c };
            return this;
        }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            if (other.GetType() != this.GetType())
                return false;

            return this.GeoCode == ((GeocodeMatrix)other).GeoCode;
        }

    }
}
