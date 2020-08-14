using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class CreateKeyspace : Query
    {
        private String keyspace;
        private ReplicationStrategy dbReplicationStrategy;

        //For SimpleStrategy
        private DataCenter[] dbDataCenters;

        /// <summary>
        /// To create keyspace queries
        /// </summary>
        public CreateKeyspace()
        {

        }


        /// <summary>
        /// Set keyspace name
        /// </summary>
        /// <param name="keyspace">Keyspace name</param>
        /// <returns>CreateKeyspace</returns>
        public CreateKeyspace Keyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        /// <summary>
        /// Set replication strategy
        /// </summary>
        /// <param name="dbReplicationStrategy">SimpleStrategy, NetworkTopologyStrategy</param>
        /// <returns>CreateKeyspace</returns>
        public CreateKeyspace ReplicationStrategy(ReplicationStrategy dbReplicationStrategy)
        {
            this.dbReplicationStrategy = dbReplicationStrategy;

            return this;
        }
        
        /// <summary>
        /// Set data center
        /// </summary>
        /// <param name="dbDataCenters">DataCenters</param>
        /// <returns>CreateKeyspace</returns>
        public CreateKeyspace DataCenters(DataCenter[] dbDataCenters)
        {
            this.dbDataCenters = dbDataCenters;

            return this;
        }

        //Returns e.g. "name text, " or "name text static, "
        private void AppendVariableRow(StringBuilder sb, Column variable)
        {
            sb.Append(variable.Name());
        }

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendVariableRows(StringBuilder sb, Column[] variables)
        {
            if (variables == null)
                return;

            for (int i = 0; i < variables.Length; i++)
            {
                AppendVariableRow(sb, variables[i]);

                if (i < variables.Length - 1)
                    sb.Append(", ");
            }
        }

        // return "CREATE KEYSPACE " + keyspace + @" WITH REPLICATION = { 'class' : 'NetworkTopologyStrategy', '" + StaticSettings.dbSettings.DataCenterName() + "' : " + StaticSettings.dbSettings.ReplicationFactor() + @" };";
        /// <summary>
        /// Creates the prepared statement string
        /// </summary>
        /// <returns>String</returns>
        public override String ToString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Name cannot be null");
            if (dbReplicationStrategy == null)
                throw new NullReferenceException("ReplicationStrategy cannot be null");
            if (dbDataCenters == null)
                throw new NullReferenceException("DataCenters cannot be null");


            StringBuilder sb = new StringBuilder();

            sb.Append("CREATE KEYSPACE " + keyspace + " WITH REPLICATION = { 'class' : '" + dbReplicationStrategy.Value + "'");

            if (Utils.CompareStrings(dbReplicationStrategy.Value, CassandraQueryBuilder.ReplicationStrategy.SimpleStrategy.Value))
            {
                if (dbDataCenters.Length != 1)
                    throw new Exception("DataCenters must contain exactly one object for SimpleStrategy");

                sb.Append(", 'replication_factor' : " + dbDataCenters[0].GetReplicationFactor()); //dbDataCenters[1].GetReplicationFactor() throws error if replication factor is not set
            }
            else if (Utils.CompareStrings(dbReplicationStrategy.Value, CassandraQueryBuilder.ReplicationStrategy.NetworkTopologyStrategy.Value))
            {
                if (dbDataCenters.Length == 0)
                    throw new Exception("DataCenters must contain at least one object for NetworkTopotogyStrategy");

                foreach(DataCenter dataCenter in dbDataCenters)
                    sb.Append(", '" + dataCenter.GetName() + "' : " + dataCenter.GetReplicationFactor()); //dbDataCenters[1].GetReplicationFactor() throws error if replication factor is not set
            }
            



            sb.Append(" };");

            

            return sb.ToString();
        }

        public String GetDropString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Name cannot be null");

            return "DROP KEYSPACE IF EXISTS " + keyspace + ";";
        }

    }
}
