using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraQueryBuilder
{
    //http://stackoverflow.com/questions/630803/associating-enums-with-strings-in-c-sharp

    //Consistency levels https://docs.datastax.com/en/cassandra/2.1/cassandra/dml/dml_config_consistency_c.html
    public class ConsistencyLevel
    {
        public ConsistencyLevel Value { get; private set; }

        private ConsistencyLevel(ConsistencyLevel value) { Value = value; }
        public static ConsistencyLevel All { get { return new ConsistencyLevel(ConsistencyLevel.All); } }
        public static ConsistencyLevel Any { get { return new ConsistencyLevel(ConsistencyLevel.Any); } }
        public static ConsistencyLevel EachQuorum { get { return new ConsistencyLevel(ConsistencyLevel.EachQuorum); } }
        public static ConsistencyLevel LocalOne { get { return new ConsistencyLevel(ConsistencyLevel.LocalOne); } }
        public static ConsistencyLevel LocalQuorum { get { return new ConsistencyLevel(ConsistencyLevel.LocalQuorum); } }
        public static ConsistencyLevel LocalSerial { get { return new ConsistencyLevel(ConsistencyLevel.LocalSerial); } }
        public static ConsistencyLevel One { get { return new ConsistencyLevel(ConsistencyLevel.One); } }
        public static ConsistencyLevel Quorum { get { return new ConsistencyLevel(ConsistencyLevel.Quorum); } }
        public static ConsistencyLevel Serial { get { return new ConsistencyLevel(ConsistencyLevel.Serial); } }
        public static ConsistencyLevel Three { get { return new ConsistencyLevel(ConsistencyLevel.Three); } }
        public static ConsistencyLevel Two { get { return new ConsistencyLevel(ConsistencyLevel.Two); } }


    }
}
