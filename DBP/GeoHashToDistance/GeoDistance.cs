using System;
namespace Proximus
{
    public class GeoDistance : IEntity
    {
        public Geocode Start { get; private set; }
        public Geocode End { get; private set; }
        public double Miles { get; private set; }
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
            if (other.GetType() != this.GetType())
                return false;
            var obj = other as GeoDistance;
            return this.Start == obj.Start &&
                this.End == obj.End;
        }
    }
}
