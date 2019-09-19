using System;
using System.Collections.Generic;
using System.IO;
using LiteDB;

namespace Proximus
{
    public class WorkflowDatastore

    {
        LiteDatabase database;
        LiteCollection<GeoCode> geoCodes;


        public WorkflowDatastore(string dir)
        {
            database = new LiteDatabase(Path.Combine(dir, "transient.db"));

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
