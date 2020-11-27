using System;

namespace CassandraQueryBuilder
{
    public class Column
    {
        private String name;
        private ColumnType columnType;
        private bool isStatic;

        public Column(String name, ColumnType columnType = null, bool isStatic = false)
        {
            this.name = name;
            this.columnType = columnType;
            this.isStatic = isStatic;
        }

        public String Name()
        {
            if(name == null)
                throw new NullReferenceException();

            return name;
        }

        public String ColumnType()
        {
            if(columnType.Value == null)
                throw new NullReferenceException();

            return columnType.Value;
        }

        public bool IsStatic()
        {
            return isStatic;
        }

    }
}
