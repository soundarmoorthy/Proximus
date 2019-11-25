using LiteDB;
using System;
namespace Proximus
{
    public class GeoDistance : IEntity
    {
        public Geocode Start { get; private set; }
        public Geocode End { get; private set; }
        public double Miles { get; private set; }

        [BsonField("Id")]
        public string Id
        {
            get{ return Start.Id + End.Id; }
            set { }
        }


        public GeoDistance()
        {
        }

        public static GeoDistance Create(string s, string e, double m)
        {
            return new GeoDistance()
            {
                Start = new Geocode() { Code = s },
                End = new Geocode() { Code = e },
                Miles = m
            };
        }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            if (! (other is GeoDistance))
                return false;
            var obj = other as GeoDistance;
            return this.Id == ((GeoDistance)other).Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

    }
}
