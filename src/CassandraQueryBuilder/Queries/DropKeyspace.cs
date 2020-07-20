using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Cassandra.QueryBuilder
{
    public class DBDropKeyspace : IQuery
    {
        private String name;

        public DBDropKeyspace()
        {

        }


        public DBDropKeyspace SetName(String keyspace)
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
