using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class DropKeyspace : Query
    {
        private String name;

        public DropKeyspace()
        {

        }


        public DropKeyspace Keyspace(String keyspace)
        {
            this.name = keyspace;

            return this;
        }

        public override String ToString()
        {
            if (name == null)
                throw new NullReferenceException("Name cannot be null");

            return "DROP KEYSPACE IF EXISTS " + name + ";";
        }

    }
}
