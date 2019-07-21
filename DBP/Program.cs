using Neo4j.Driver.V1;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.IO;

namespace DBP
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = args[0];


            var geoHashPopulator = new GeoHashPopulator();

            using (var rd = new StreamReader(file))
            {

                while (!rd.EndOfStream)
                {
                    var geoHash = rd.ReadLine();
                    geoHashPopulator.InsertIntoDbAsync(geoHash);                                       
                }
            }
        }
    }
}
