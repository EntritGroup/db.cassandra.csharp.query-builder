using System;

namespace CassandraQueryBuilder
{
    public class DropKeyspace : Query
    {
        private String name;

        /// <summary>
        /// To create DROP KEYSPACE queries
        /// </summary>
        public DropKeyspace()
        {

        }


        /// <summary>
        /// Set keyspace name
        /// </summary>
        /// <param name="keyspace">Keyspace name</param>
        /// <returns>DropKeyspace</returns>
        public DropKeyspace Keyspace(String keyspace)
        {
            this.name = keyspace;

            return this;
        }

        /// <summary>
        /// Creates the prepared statement string
        /// </summary>
        /// <returns>String</returns>
        public override String ToString()
        {
            if (name == null)
                throw new NullReferenceException("Keyspace name cannot be null");

            return "DROP KEYSPACE IF EXISTS " + name + ";";
        }

    }
}
