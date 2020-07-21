using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB.CassandraQueryBuilder
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

        public static ColumnType BOOLEAN { get { return new ColumnType("BOOLEAN"); } }
        public static ColumnType DOUBLE { get { return new ColumnType("DOUBLE"); } }
        public static ColumnType INTEGER { get { return new ColumnType("INT"); } }
        public static ColumnType TEXT { get { return new ColumnType("TEXT"); } }
        public static ColumnType TIMESTAMP { get { return new ColumnType("TIMESTAMP"); } }
        public static ColumnType TIMEUUID { get { return new ColumnType("TIMEUUID"); } }
        public static ColumnType UUID { get { return new ColumnType("UUID"); } }
        public static ColumnType COUNTER { get { return new ColumnType("COUNTER"); } }
        public static ColumnType INET { get { return new ColumnType("INET"); } }

        public static ColumnType LIST(ColumnType dbColumnType) { return new ColumnType("LIST<" + dbColumnType.Value + ">", true); } //e.g. LIST<TEXT>

        //http://datastax.github.io/csharp-driver/features/datatypes/
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
