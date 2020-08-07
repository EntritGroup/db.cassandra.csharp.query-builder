namespace CassandraQueryBuilder
{
    //http://stackoverflow.com/questions/630803/associating-enums-with-strings-in-c-sharp

    //https://cassandra.apache.org/doc/latest/cql/functions.html
    public class SelectAggregate
    {
        public string Value { get; private set; }

        private SelectAggregate(string value) { Value = value; }

        public static SelectAggregate COUNT { get { return new SelectAggregate("COUNT"); } }

        public static SelectAggregate SUM { get { return new SelectAggregate("SUM"); } }
        
        public static SelectAggregate AVG { get { return new SelectAggregate("AVG"); } }

        public static SelectAggregate MAX { get { return new SelectAggregate("MAX"); } }

        public static SelectAggregate MIN { get { return new SelectAggregate("MIN"); } }

    }
}
