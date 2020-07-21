using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.CassandraQueryBuilder
{
    public class UpdateQuery// : IPreparedStatement
    {
        private String keyspace;
        private String tableName;
        private Column[] variables;
        private Column[] whereVariables;
        private Boolean ttl = false; //Om man har ttl så ska den ligga sist i valuesVariables;
        private Boolean ifExists = false;
        private Boolean setTimestamp = false;

        //private Object preparedStatmentLock = new Object();
        //private PreparedStatement preparedStatement;
        private ConsistencyLevel consistencyLevel;

        private ListUpdateType listUpdateType;

        public UpdateQuery()
        {

        }


        public UpdateQuery SetKeyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        public UpdateQuery SetTableName(String tableName)
        {
            this.tableName = tableName;

            return this;
        }

        public UpdateQuery SetVariables(params Column[] variables)
        {
            this.variables = variables;

            return this;
        }

        public UpdateQuery SetWhereVariables(params Column[] whereVariables)
        {
            this.whereVariables = whereVariables;

            return this;
        }

        //Om man har ttl så ska den ligga sist i valuesVariables
        public UpdateQuery SetTTL()
        {
            this.ttl = true;

            return this;
        }

        public UpdateQuery SetIfExists()
        {
            this.ifExists = true;

            return this;
        }

        public UpdateQuery SetTimestamp()
        {
            this.setTimestamp = true;

            return this;
        }

        public UpdateQuery SetConsistencyLevel(ConsistencyLevel consistencyLevel)
        {
            this.consistencyLevel = consistencyLevel;

            return this;
        }

        public UpdateQuery SetListUpdateType(ListUpdateType listUpdateType)
        {
            this.listUpdateType = listUpdateType;

            return this;
        }





        //Returns e.g. "name text, " or "name text static, "
        private void AppendVariableRow(StringBuilder sb, Column variable)
        {
            if(variable.GetColumnType().StartsWith("LIST<"))
            {
                if (listUpdateType == ListUpdateType.PREPEND)
                    sb.Append(variable.GetName() + " = ? + " + variable.GetName());
                else if (listUpdateType == ListUpdateType.APPEND)
                    sb.Append(variable.GetName() + " = " + variable.GetName() + " + ?");
                else if (listUpdateType == ListUpdateType.REPLACE_ALL)
                    sb.Append(variable.GetName() + " = ?");
                else
                    sb.Append(variable.GetName()+ "[?] = ?");
            }
            else
                sb.Append(variable.GetName() + " = ?");
        }

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendVariableRows(StringBuilder sb, Column[] variables, String delimiter)
        {
            if (variables == null)
                return;

            for (int i = 0; i < variables.Length; i++)
            {
                AppendVariableRow(sb, variables[i]);

                if (i < variables.Length - 1)
                    sb.Append(delimiter + " ");
            }
        }

        //Om man har ttl så ska den ligga sist i valuesVariables
        //UPDATE ks.tb SET v1 = ?, v2 = ? WHERE v1 = ? AND v2 = ? IF EXISTS;
        public String GetString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (tableName == null)
                throw new NullReferenceException("TableName cannot be null");
            if (variables == null)
                throw new NullReferenceException("Variables cannot be null");
            if (whereVariables == null)
                throw new NullReferenceException("WhereVariables cannot be null");



            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE " + keyspace + "." + tableName);

            if (setTimestamp || ttl)
            {
                sb.Append(" USING");

                if (setTimestamp)
                    sb.Append(" TIMESTAMP ?");

                if (setTimestamp && ttl)
                    sb.Append(" AND");

                if (ttl)
                    sb.Append(" TTL ?");
            }

            sb.Append(" SET ");

            AppendVariableRows(sb, variables, ",");


            sb.Append(" WHERE ");


            AppendVariableRows(sb, whereVariables, " AND");
            

            if (ifExists)
                sb.Append(" IF EXISTS");


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
