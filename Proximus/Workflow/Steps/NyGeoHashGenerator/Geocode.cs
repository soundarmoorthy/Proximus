using System;
using System.Collections.Generic;

namespace Proximus
{
    public class Geocode : IEntity, IEquatable<Geocode>
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

        public bool Equals(Geocode other)
        {
            if (other == null)
                return false;

            return this.Code == other.Code;
        }
        public override string ToString()
        {
            return Code;
        }
    }
}
