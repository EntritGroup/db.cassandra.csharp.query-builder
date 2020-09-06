namespace CassandraQueryBuilder
{
    public class ReplicationStrategy
    {
        public string Value { get; private set; }

        private ReplicationStrategy(string value) { Value = value; }

        public static ReplicationStrategy SimpleStrategy { get { return new ReplicationStrategy("SimpleStrategy"); } }
        public static ReplicationStrategy NetworkTopologyStrategy { get { return new ReplicationStrategy("NetworkTopologyStrategy"); } }


    }
}
