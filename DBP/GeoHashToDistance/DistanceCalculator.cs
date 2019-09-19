using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Proximus
{
    public class DistanceCalculator : WorkflowStep
    {
        
        public DistanceCalculator(WorkflowState state) : base(state)
        {

        }

        public override void Start()
        {
            this.ProcessGeoHashToDistance();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        IDictionary<int, ConnectionMultiplexer> connections = new Dictionary<int, ConnectionMultiplexer>();

        public override string Name => "Calculate Distance";

        private async Task Process(string filename, int pooler)
        {
            try
            {
                var conn = connections[pooler];
                IDatabase db = conn.GetDatabase();

                var ghp = new GeoHashPopulator();
                var rd = new StreamReader(filename);
                while (!rd.EndOfStream)
                {
                    var values = rd.ReadLine().Split(",");
                    for (int i = 1; i < values.Length; i++)
                    {
                        var key = $"{values[0].Trim()}:{values[i].Trim()}";
                        if (db.KeyExists(key))
                        {
                            this.State.Logger.Log($"Found Key {key}");
                            continue;
                        }
                        var distance = await ghp.FindDistance(values[0], values[i]);
                        db.StringSet(key, distance);
                        this.State.Logger.Log($"Added {key},{distance}");
                    }
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }

        private void ProcessGeoHashToDistance()
        {
            var files = Directory.GetFiles("", "*.txt");
            var len = files.Length;
            Task[] tasks = new Task[len];
            InitializeMultiplexers(len);
            for (int i = 0; i < len; i++)
            {
                var fileName = files[i];
                var index = i / 2;
                var task = Process(fileName, index);
                tasks[i] = task;
            }

            Task.WaitAll(tasks);

            foreach (var conn in connections.Values)
            {
                conn.Close();
            }
        }


        private void InitializeMultiplexers(int count)
        {
            for (int i = 0; i < count / 2; i++)
            {
                connections.Add(i, ConnectionMultiplexer.Connect("localhost:6379"));
            }
        }

    }
}
