using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CassandraQueryBuilder;

namespace CassandraQueryBuilder.Tests.UT
{
    public class Columns
    {
        internal static readonly Column partitionKey1 = new Column("pk1", ColumnType.TEXT);
        internal static readonly Column partitionKey2 = new Column("pk2", ColumnType.TEXT);
        internal static readonly Column clusteringKey1 = new Column("ck1", ColumnType.TEXT);
        internal static readonly Column clusteringKey2 = new Column("ck2", ColumnType.TEXT);
        internal static readonly Column clusteringKey3 = new Column("ck3", ColumnType.TEXT);
        internal static readonly Column columns1 = new Column("v1", ColumnType.TEXT);
        internal static readonly Column columns2 = new Column("v2", ColumnType.TEXT);
        internal static readonly Column columns3 = new Column("v3", ColumnType.TEXT);
        internal static readonly Column columns_list1 = new Column("vl1", ColumnType.LIST(ColumnType.TEXT));
        internal static readonly Column columns4_STATIC = new Column("v4", ColumnType.TEXT, true);
        internal static readonly Column columnsCounter = new Column("counter_column_name", ColumnType.COUNTER);
        
    }
}
