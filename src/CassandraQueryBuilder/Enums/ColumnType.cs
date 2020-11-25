using System;

namespace CassandraQueryBuilder
{
    public class ColumnType
    {
        public string Value { get; private set; }

        public ColumnType(string value)
        {
            Value = value;
        }

        public static ColumnType ASCII { get { return new ColumnType("ASCII"); } }
        public static ColumnType BIGINT { get { return new ColumnType("BIGINT"); } }
        public static ColumnType BLOB { get { return new ColumnType("BLOB"); } }
        public static ColumnType BOOLEAN { get { return new ColumnType("BOOLEAN"); } }
        public static ColumnType COUNTER { get { return new ColumnType("COUNTER"); } }
        public static ColumnType DATE { get { return new ColumnType("DATE"); } }
        public static ColumnType DECIMAL { get { return new ColumnType("DECIMAL"); } }
        public static ColumnType DOUBLE { get { return new ColumnType("DOUBLE"); } }
        public static ColumnType DURATION { get { return new ColumnType("DURATION"); } }
        public static ColumnType FLOAT { get { return new ColumnType("FLOAT"); } }
        public static ColumnType INET { get { return new ColumnType("INET"); } }
        public static ColumnType INT { get { return new ColumnType("INT"); } }
        public static ColumnType SMALLINT { get { return new ColumnType("SMALLINT"); } }
        public static ColumnType TEXT { get { return new ColumnType("TEXT"); } }
        public static ColumnType TIME { get { return new ColumnType("TIME"); } }
        public static ColumnType TIMESTAMP { get { return new ColumnType("TIMESTAMP"); } }
        public static ColumnType TIMEUUID { get { return new ColumnType("TIMEUUID"); } }
        public static ColumnType TINYINT { get { return new ColumnType("TINYINT"); } }
        public static ColumnType UUID { get { return new ColumnType("UUID"); } }
        public static ColumnType VARCHAR { get { return new ColumnType("VARCHAR"); } }
        public static ColumnType VARINT { get { return new ColumnType("VARINT"); } }

        //Collections
        public static ColumnType MAP(ColumnType columnType1, ColumnType columnType2) { return new ColumnType("MAP<" + columnType1.Value + ", " + columnType2.Value + ">"); } //e.g. MAP<TEXT, TEXT>
        public static ColumnType SET(ColumnType columnType) { return new ColumnType("SET<" + columnType.Value + ">"); } //e.g. SET<TEXT>
        public static ColumnType LIST(ColumnType columnType) { return new ColumnType("LIST<" + columnType.Value + ">"); } //e.g. LIST<TEXT>

        //Other
        public static ColumnType FROZEN(ColumnType columnType) { return new ColumnType("FROZEN<" + columnType.Value + ">"); } //e.g. FROZEN<LIST<TEXT>>
        public static ColumnType TUPLE(ColumnType[] columnType) { return new ColumnType("TUPLE<" + GetTupleList(columnType) + ">"); } //e.g. TUPLE<TEXT, TEXT>>


        private static String GetTupleList(ColumnType[] columnType)
        {
            if (columnType == null || columnType.Length == 0)
                throw new ArgumentNullException();
         
            String ret = "";
               
            for (int i = 0; i < columnType.Length; i++)
            {
                if(i == columnType.Length-1)
                    ret = ret + columnType[i].Value;
                else
                    ret = ret + columnType[i].Value + ", ";
            }

            return ret;
        }

        //https://cassandra.apache.org/doc/latest/cql/types.html
        //CQL3 data type    C# type
        //ascii             string
        //bigint            long
        //blob              byte[]
        //boolean           bool
        //counter           long
        //custom            byte[]
        //date              LocalDate
        //decimal           decimal
        //double            double
        //float             float
        //inet              IPAddress
        //int               int
        //list              IEnumerable<T>
        //map               IDictionary<K, V>
        //set               IEnumerable<T>
        //smallint          short
        //text              string
        //time              LocalTime
        //timestamp         DateTimeOffset
        //timeuuid          TimeUuid
        //tinyint           sbyte
        //uuid              Guid
        //varchar           string
        //varint            BigInteger

    }
}
