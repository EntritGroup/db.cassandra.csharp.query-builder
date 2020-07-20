using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Cassandra.QueryBuilder
{
    public class DBCreateKeyspace : IQuery
    {
        private String name;
        private ReplicationStrategy dbReplicationStrategy;

        //For SimpleStrategy
        private DBDataCenter[] dbDataCenters;

        public DBCreateKeyspace()
        {

        }


        public DBCreateKeyspace SetName(String keyspace)
        {
            this.name = keyspace;

            return this;
        }

        public DBCreateKeyspace SetReplicationStrategy(ReplicationStrategy dbReplicationStrategy)
        {
            this.dbReplicationStrategy = dbReplicationStrategy;

            return this;
        }
        
        public DBCreateKeyspace SetDataCenters(DBDataCenter[] dbDataCenters)
        {
            this.dbDataCenters = dbDataCenters;

            return this;
        }

        //Returns e.g. "name text, " or "name text static, "
        private void AppendVariableRow(StringBuilder sb, DBColumn variable)
        {
            sb.Append(variable.GetName());
        }

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendVariableRows(StringBuilder sb, DBColumn[] variables)
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

        //Om man har ttl så ska den ligga sist i valuesVariables
        // return "CREATE KEYSPACE " + keyspace + @" WITH REPLICATION = { 'class' : 'NetworkTopologyStrategy', '" + StaticSettings.dbSettings.DataCenterName() + "' : " + StaticSettings.dbSettings.ReplicationFactor() + @" };";
        public String GetString()
        {
            if (name == null)
                throw new NullReferenceException("Name cannot be null");
            if (dbReplicationStrategy == null)
                throw new NullReferenceException("DBReplicationStrategy cannot be null");
            if (dbDataCenters == null)
                throw new NullReferenceException("DBDataCenters cannot be null");


            StringBuilder sb = new StringBuilder();

            sb.Append("CREATE KEYSPACE " + name + " WITH REPLICATION = { 'class' : '" + dbReplicationStrategy.Value + "'");

            if (Utils.CompareStrings(dbReplicationStrategy.Value, ReplicationStrategy.SimpleStrategy.Value))
            {
                if (dbDataCenters.Length != 1)
                    throw new Exception("DBDataCenters must contain exactly one object for SimpleStrategy");

                sb.Append(", 'replication_factor' : " + dbDataCenters[0].GetReplicationFactor()); //dbDataCenters[1].GetReplicationFactor() throws error if replication factor is not set
            }
            else if (Utils.CompareStrings(dbReplicationStrategy.Value, ReplicationStrategy.NetworkTopologyStrategy.Value))
            {
                if (dbDataCenters.Length == 0)
                    throw new Exception("DBDataCenters must contain at least one object for NetworkTopotogyStrategy");

                foreach(DBDataCenter dataCenter in dbDataCenters)
                    sb.Append(", '" + dataCenter.GetName() + "' : " + dataCenter.GetReplicationFactor()); //dbDataCenters[1].GetReplicationFactor() throws error if replication factor is not set
            }
            



            sb.Append(" };");

            

            return sb.ToString();
        }

        public String GetDropString()
        {
            if (name == null)
                throw new NullReferenceException("Name cannot be null");

            return "DROP KEYSPACE IF EXISTS " + name + ";";
        }

    }
}
