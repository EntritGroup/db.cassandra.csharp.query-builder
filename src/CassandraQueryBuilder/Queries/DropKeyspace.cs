﻿using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class DropKeyspace : IQuery
    {
        private String name;

        public DropKeyspace()
        {

        }


        public DropKeyspace SetName(String keyspace)
        {
            this.name = keyspace;

            return this;
        }

        public String GetString()
        {
            if (name == null)
                throw new NullReferenceException("Name cannot be null");

            return "DROP KEYSPACE IF EXISTS " + name + ";";
        }

    }
}
