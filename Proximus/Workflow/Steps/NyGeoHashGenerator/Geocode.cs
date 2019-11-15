using System;
using LiteDB;
using System.Collections.Generic;

namespace Proximus
{
    public class Geocode : IEntity, IEquatable<Geocode>
    {
        [BsonField("Id")]
        public string Code { get; set; }
        public Geocode()
        {
        }

        private static Geocode none;
        public static Geocode None
        {
            get
            {
                if (none == null)
                    none = new Geocode() { Code = NoneCode };
                return none;
            }
        }

        private const string NoneCode = "!@#$%^&*";

        public bool Equals(Geocode other)
        {
            if (other == null)
                return false;
            return this.Code == other.Code;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Geocode))
                return false;

            return this.Id == (obj as Geocode).Id;
        }

        public override int GetHashCode()
        {
            var hash = this.Code.GetHashCode();
            return hash;
        }


        public override string ToString()
        {
            return Id;
        }

        public string Id
        {
            get
            {
                return Code;
            }
        }
    }
}
