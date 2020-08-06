using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraQueryBuilder
{
    //http://stackoverflow.com/questions/630803/associating-enums-with-strings-in-c-sharp

    //CQL types https://docs.datastax.com/en/cql/3.0/cql/cql_reference/cql_data_types_c.html
    public class ColumnType
    {
        public string Value { get; private set; }
        public bool IsCollection { get; private set; }

        private ColumnType(string value, bool isCollection = false)
        {
            Value = value;
            IsCollection = isCollection;
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
        public static ColumnType MAP(ColumnType dbColumnType1, ColumnType dbColumnType2) { return new ColumnType("MAP<" + dbColumnType1.Value + ", " + dbColumnType2.Value + ">", true); } //e.g. MAP<TEXT, TEXT>
        public static ColumnType SET(ColumnType dbColumnType) { return new ColumnType("SET<" + dbColumnType.Value + ">", true); } //e.g. SET<TEXT>
        public static ColumnType LIST(ColumnType dbColumnType) { return new ColumnType("LIST<" + dbColumnType.Value + ">", true); } //e.g. LIST<TEXT>

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
