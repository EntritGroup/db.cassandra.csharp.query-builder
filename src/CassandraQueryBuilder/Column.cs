﻿using System;

namespace CassandraQueryBuilder
{
    public class Column
    {
        String name;
        ColumnType columnType;
        bool isStatic;

        public Column(String name, ColumnType columnType, bool isStatic = false)
        {
            this.name = name;
            this.columnType = columnType;
            this.isStatic = isStatic;
        }

        public String Name()
        {
            return name;
        }

        public String ColumnType()
        {
            return columnType.Value;
        }

        public bool IsStatic()
        {
            return isStatic;
        }

    }
}
