using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Proximus
{
    public class Neo4jFileCreator : WorkflowStep
    { 
        public Neo4jFileCreator(WorkflowState state) : base(state)
        {
        }

        public override string Name =>    "Create Files for Neo4j import";

        public override void Start()
        {
            var task  = CreateFilesForNeo4JImport();
            task.Wait();
        }

        public override void Stop()
        {
        }

        private async Task CreateFilesForNeo4JImport()
        {
            //This is the redis service 
            var conn = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = conn.GetDatabase();
            //These file names and the folder names will be used in the neo4j-admin command
            //when importing nodes and edges into the graph database. If you change these
            //you need to change the corresponding names in the import command.
            var dir = "";
            var nodesFile = new StreamWriter(System.IO.Path.Combine(dir,"nodes.csv"));
            var edgesFile = new StreamWriter(System.IO.Path.Combine(dir,"edges.csv"));
            nodesFile.AutoFlush = edgesFile.AutoFlush = true;


            //This is the redis service which is expected to run locally
            var keys = conn.GetServer("localhost:6379").Keys();
            HashSet<string> set = new HashSet<string>();
            int j = 0;
            foreach (var key in keys)
            {
                var nodes = ((string)key).Split(':');
                var start = nodes[0];
                var end = nodes[1];
                var distance = db.StringGet(key);

                if (!set.Contains(start))
                    set.Add(start);
                if (!set.Contains(end))
                    set.Add(end);
                //The keyword ROAD will be referred by the edges_header.csv file in the assets folder.
                await edgesFile.WriteLineAsync($"{start},{distance},{end},ROAD");
                if (j % 1000 == 0)
                {
                    this.State.Logger.Log($"{j} out of unknown records complete");
                }
                j++;
            }

            foreach (var key in set)
            {
                //The keywork GEOHASH will be referred by the nodes_header.csv file in the assets folder.
                await nodesFile.WriteLineAsync($"{key},GEOHASH");
            }

            set.Clear();
            nodesFile.Close();
            edgesFile.Close();
            conn.Close();
        }
    }
}
