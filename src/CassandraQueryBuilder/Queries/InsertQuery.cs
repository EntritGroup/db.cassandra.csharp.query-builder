using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraQueryBuilder
{
    public class InsertQuery// : IPreparedStatement
    {
        private String keyspace;
        private String tableName;
        private Column[] variables;
        private Boolean ttl = false; //Om man har ttl så ska den ligga sist i valuesVariables
        private Boolean ifNotExists = false;
        private Boolean setTimestamp = false;

        //private Object preparedStatmentLock = new Object();
        //private PreparedStatement preparedStatement;
        // private ConsistencyLevel consistencyLevel;

        public InsertQuery()
        {

        }


        public InsertQuery SetKeyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        public InsertQuery SetTableName(String tableName)
        {
            this.tableName = tableName;

            return this;
        }

        public InsertQuery SetColumns(params Column[] variables)
        {
            this.variables = variables;

            return this;
        }

        //Om man har ttl så ska den ligga sist i valuesVariables
        public InsertQuery SetTTL()
        {
            this.ttl = true;

            return this;
        }

        public InsertQuery SetIfNotExists()
        {
            this.ifNotExists = true;

            return this;
        }

        public InsertQuery SetTimestamp()
        {
            this.setTimestamp = true;

            return this;
        }


        //Returns e.g. "name text, " or "name text static, "
        private void AppendVariableRow(StringBuilder sb, Column variable)
        {
            sb.Append(variable.GetName());
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
        
        //Om man har ttl så ska den ligga sist i valuesVariables
        //INSERT INTO ks.tb (v1, v2) VALUES (?, ?) IF NOT EXISTS USING TTL ?;
        public String GetString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (tableName == null)
                throw new NullReferenceException("TableName cannot be null");
            if (variables == null)
                throw new NullReferenceException("Variables cannot be null");



            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO " + keyspace + "." + tableName + " (");

            
            AppendVariableRows(sb, variables);


            sb.Append(") VALUES (");


            for (int i = 0; i < variables.Length; i++)
            {
                if (i > 0)
                    sb.Append(", ");
                sb.Append("?");
            }

            sb.Append(")");



            if (ifNotExists)
                sb.Append(" IF NOT EXISTS");

            if (setTimestamp || ttl)
            {
                sb.Append(" USING");

                if (setTimestamp)
                    sb.Append(" TIMESTAMP ?");

                if(setTimestamp && ttl)
                    sb.Append(" AND");

                if (ttl)
                    sb.Append(" TTL ?");
            }

            sb.Append(";");

            

            return sb.ToString();
        }


        /*public async Task<PreparedStatement> GetPreparedStatement()
        {
            if (preparedStatement != null)
                return preparedStatement;

            await Task.Run(() => PrepareStatement());

            return preparedStatement;
        }

        private void PrepareStatement()
        {
            lock (preparedStatmentLock)
            {
                //To make sure that if multiple threads are calling this at the same time. If two have passed the if statement above, then the second one getting into this will just return the prepared statement and not create another one
                if (preparedStatement != null)
                    return;

                //Using the p below since in the GetPraparedStatement, the preparedStatement should not be set before by first setting it and then afterwards setting the consistency level
                PreparedStatement p = DB.CreatePreparedStatement(GetString());

                if (consistencyLevel != null)
                    p.SetConsistencyLevel(consistencyLevel.Value);

                preparedStatement = p;

            }
        }*/


    }
}
