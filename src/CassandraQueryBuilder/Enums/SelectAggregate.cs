namespace CassandraQueryBuilder
{
    public class SelectAggregate
    {
        public string Value { get; private set; }

        public SelectAggregate(string value) { Value = value; }

        public static SelectAggregate COUNT { get { return new SelectAggregate("COUNT"); } }

        public static SelectAggregate SUM { get { return new SelectAggregate("SUM"); } }
        
        public static SelectAggregate AVG { get { return new SelectAggregate("AVG"); } }

        public static SelectAggregate MAX { get { return new SelectAggregate("MAX"); } }

        public static SelectAggregate MIN { get { return new SelectAggregate("MIN"); } }

    }
}
