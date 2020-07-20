using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Cassandra.QueryBuilder
{
    //http://stackoverflow.com/questions/630803/associating-enums-with-strings-in-c-sharp

    //Replication strategies http://docs.datastax.com/en/cql/3.1/cql/cql_reference/create_keyspace_r.html
    public class ReplicationStrategy
    {
        public string Value { get; private set; }

        private ReplicationStrategy(string value) { Value = value; }

        public static ReplicationStrategy SimpleStrategy { get { return new ReplicationStrategy("SimpleStrategy"); } }
        public static ReplicationStrategy NetworkTopologyStrategy { get { return new ReplicationStrategy("NetworkTopologyStrategy"); } }


    }
}
