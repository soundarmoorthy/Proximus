using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using LiteDB;

namespace Proximus
{
    public class WorkflowDatastore

    {
        LiteDatabase database;
        LiteCollection<GeoCode> geoCodes;


        public WorkflowDatastore(string dir)
        {
            var file = Path.Combine(dir, "transient.db");
            database = new LiteDatabase(file);
            geoCodes = database.GetCollection<GeoCode>();
        }

        public IEnumerable<GeoCode> Geocodes()
        {
                return geoCodes.Query().ToEnumerable();
        }

        public void Add(GeoCode code)
        {
                    geoCodes.Insert(code);
        }
    }
}
