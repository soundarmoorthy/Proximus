using System;
using System.Linq;
using System.Collections.Generic;

namespace Proximus
{
    public class NodeNeighbour
    {
        public NodeNeighbour()
        {
        }

        const string base32 = "0123456789bcdefghjkmnpqrstuvwxyz"; // (geohash-specific) Base32 map


        /**
  * Returns all 8 adjacent cells to specified geohash.
  *
  * @param   {string} geohash - Geohash neighbours are required of.
  * @returns {{n,ne,e,se,s,sw,w,nw: string}}
  * @throws  Invalid geohash.
  */
        public static GeocodeMatrix Neighbours(string code)
        {
            return GeocodeMatrix.Create(code)
                .Add(Direction.N, adjacent(code, Direction.N))
                .Add(Direction.NE, adjacent(adjacent(code, Direction.N), Direction.E))
                .Add(Direction.E, adjacent(code, Direction.E))
                .Add(Direction.SE, adjacent(adjacent(code, Direction.S), Direction.E))
                .Add(Direction.S, adjacent(code, Direction.S))
                .Add(Direction.SW, adjacent(adjacent(code, Direction.S), Direction.W))
                .Add(Direction.W, adjacent(code, Direction.W))
                .Add(Direction.NW, adjacent(adjacent(code, Direction.N), Direction.W));
        }


        private static Geocode adjacent(Geocode geoCode, Direction d)
        {
            return Geocode.None;
        }

        /**
    * Determines adjacent cell in given direction.
    *
    * @param   geohash - Cell to which adjacent cell is required.
    * @param   direction - Direction from geohash (N/S/E/W).
    * @returns {string} Geocode of adjacent cell.
    * @throws  Invalid geohash.
    */
        private static string adjacent(string geohash, Direction d)
        {
            // based on github.com/davetroy/geohash-js
            var direction = Enum.GetName(typeof(Direction), d)[0];

            if (geohash.Length == 0)
                throw new Exception("Invalid geohash");

            if ("nsew".IndexOf(direction, StringComparison.InvariantCultureIgnoreCase) == -1)
                throw new Exception("Invalid direction");

            Dictionary<char, string[]> neighbour = new Dictionary<char, string[]> {
                { 'n', new[] { "p0r21436x8zb9dcf5h7kjnmqesgutwvy", "bc01fg45238967deuvhjyznpkmstqrwx" } },
                { 's', new[] { "14365h7k9dcfesgujnmqp0r2twvyx8zb", "238967debc01fg45kmstqrwxuvhjyznp" } },
                { 'e', new[] { "bc01fg45238967deuvhjyznpkmstqrwx", "p0r21436x8zb9dcf5h7kjnmqesgutwvy" } },
                { 'w', new[] { "238967debc01fg45kmstqrwxuvhjyznp", "14365h7k9dcfesgujnmqp0r2twvyx8zb" } },
            };

            Dictionary<char, string[]> border = new Dictionary<char, string[]>{
            { 'n', new[] { "prxz","bcfguvyz" } },
            { 's', new[]{ "028b","0145hjnp" } },
            { 'e', new[] { "bcfguvyz","prxz" } },
            { 'w', new[]{ "0145hjnp","028b" } },
            };

            var lastCh = geohash.Last();    // last character of hash
            var parent = geohash.Take(geohash.Length - 2).ToString();

            var type = geohash.Length % 2;

        // check for edge-cases which don't share common prefix
        if (border[direction][type].IndexOf(lastCh) != -1 && parent != "") {
            parent = adjacent(parent, d);
        }

        // append letter for direction to parent
        return parent + base32.ElementAt(neighbour[direction][type].IndexOf(lastCh));
    }

    }
}
