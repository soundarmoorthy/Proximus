using System;
using System.Collections.Generic;

namespace Proximus
{
    public class Geocode : IEntity
    {
        public string Code { get; set; }
        public Geocode()
        {
        }

        public static Geocode None
        {
            get
            {
                return new Geocode() { Code = null };
            }
        }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;
            if (other.GetType() != this.GetType())
                return false;

            return this.Code == ((Geocode)other).Code;
        }
    }
}
