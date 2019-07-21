using Neo4j.Driver.V1;
using Neo4jClient;
using System;
using System.Threading.Tasks;

namespace DBP
{
    public class GeoHashPopulator
    {
        public async Task InsertIntoDbAsync(string geoHash)
        {
            var client = new GraphClient(new Uri("http://atxcmrwapp-q04.devid.local:52004/db/data"), "neo4j", "password");
            client.Connect();

            var driver = GraphDatabase.Driver("bolt://atxcmrwapp-q04.devid.local:52008/db/data", AuthTokens.Basic("neo4j", "password"));

            using (var session = driver.Session())
            {
                await client.Cypher
                    .Create("(a:Geo {Hash})")
                    .WithParam("Hash", new { Hash = geoHash })
                    .ExecuteWithoutResultsAsync();
            }               
        }
    }
}
