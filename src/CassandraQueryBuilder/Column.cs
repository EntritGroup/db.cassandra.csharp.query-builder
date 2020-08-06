using System;

namespace CassandraQueryBuilder
{
    public class Column
    {
        String name;
        ColumnType dbVariableType;
        bool isStatic;

        public Column(String name, ColumnType dbColumnType, bool isStatic = false)
        {
            this.name = name;
            this.dbVariableType = dbColumnType;
            this.isStatic = isStatic;
        }

        public String GetName()
        {
            return name;
        }

        public String GetColumnType()
        {
            return dbVariableType.Value;
        }

        public bool IsStatic()
        {
            return isStatic;
        }

    }
}
