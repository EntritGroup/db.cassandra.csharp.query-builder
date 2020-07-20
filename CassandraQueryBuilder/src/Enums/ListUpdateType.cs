using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Cassandra.QueryBuilder
{
    //http://stackoverflow.com/questions/630803/associating-enums-with-strings-in-c-sharp

    //CQL types https://docs.datastax.com/en/cql/3.0/cql/cql_reference/cql_data_types_c.html
    //public class DBListUpdateType
    //{
    //    //public string Value { get; private set; }

    //    //private DBListUpdateType(string value)
    //    //{
    //    //    Value = value;
    //    //}

    //    //public static DBListUpdateType PREPEND { get { return new DBListUpdateType("PREPEND"); } }
    //    //public static DBListUpdateType APPEND { get { return new DBListUpdateType("APPEND"); } }
    //    //public static DBListUpdateType SPECIFY { get { return new DBListUpdateType("SPECIFY"); } }


    //    //public static bool IsDBListUpdateType(String listUpdateType, DBListUpdateType compareTo)
    //    //{
    //    //    if (App.Common.Utils.CompareStrings(listUpdateType, compareTo.Value))
    //    //        return true;
    //    //    return false;
    //    //}

    //}


    public enum ListUpdateType
    {
        PREPEND,
        APPEND,
        REPLACE_ALL,
        SPECIFY_INDEX_TO_OVERWRITE
    }
}
