namespace CassandraQueryBuilder
{
    public class SelectFunction
    {
        public string Value { get; private set; }

        public SelectFunction(string value) { Value = value; }

        public static SelectFunction TTL { get { return new SelectFunction("TTL"); } }

    }
}
