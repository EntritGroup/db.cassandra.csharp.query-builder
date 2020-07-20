using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Cassandra.QueryBuilder
{
    public class DBColumn
    {
        String name;
        ColumnType dbVariableType;
        bool isStatic;

        public DBColumn(String name, ColumnType dbColumnType, bool isStatic = false)
        {
            this.name = name;
            this.dbVariableType = dbColumnType;
            this.isStatic = isStatic;
        }

        public String GetName()
        {
            return name;
        }

        public String GetDBColumnType()
        {
            return dbVariableType.Value;
        }

        public bool IsStatic()
        {
            return isStatic;
        }

    }
}
