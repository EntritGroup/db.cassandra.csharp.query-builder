namespace CassandraQueryBuilder
{
    public class SelectFunction
    {
        public string Value { get; private set; }

        public SelectFunction(string value) { Value = value; }

        public static SelectFunction TODATE { get { return new SelectFunction("TODATE"); } }
        public static SelectFunction TOKEN { get { return new SelectFunction("TOKEN"); } }
        public static SelectFunction TOTIMESTAMP { get { return new SelectFunction("TOTIMESTAMP"); } }
        public static SelectFunction TOUNIXTIMESTAMP { get { return new SelectFunction("TOUNIXTIMESTAMP"); } }
        public static SelectFunction TTL { get { return new SelectFunction("TTL"); } }
        public static SelectFunction WRITETIME { get { return new SelectFunction("WRITETIME"); } }

    }
}
