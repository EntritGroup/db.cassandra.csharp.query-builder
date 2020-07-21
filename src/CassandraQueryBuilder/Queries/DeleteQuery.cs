using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraQueryBuilder
{
    public class DeleteQuery : IPreparedStatement
    {
        private String keyspace;
        private String tableName;
        private Column[] variables;
        private Column[] whereVariables;
        private Boolean ifExists = false;
        private Boolean setTimestamp = false;

        //private Object preparedStatmentLock = new Object();
        //private PreparedStatement preparedStatement;
        private ConsistencyLevel consistencyLevel;

        private ListDeleteType listDeleteType;

        public DeleteQuery()
        {

        }


        public DeleteQuery SetKeyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        public DeleteQuery SetTableName(String tableName)
        {
            this.tableName = tableName;

            return this;
        }

        public DeleteQuery SetVariables(params Column[] variables)
        {
            this.variables = variables;

            return this;
        }

        public DeleteQuery SetWhereVariables(params Column[] whereVariables)
        {
            this.whereVariables = whereVariables;

            return this;
        }

        public DeleteQuery SetIfExists()
        {
            this.ifExists = true;

            return this;
        }

        public DeleteQuery SetTimestamp()
        {
            this.setTimestamp= true;

            return this;
        }

        public DeleteQuery SetConsistencyLevel(ConsistencyLevel consistencyLevel)
        {
            this.consistencyLevel = consistencyLevel;

            return this;
        }

        public DeleteQuery SetListDeleteType(ListDeleteType listDeleteType)
        {
            this.listDeleteType = listDeleteType;

            return this;
        }



        //Returns e.g. "name text, " or "name text static, "
        private void AppendVariableRow(StringBuilder sb, Column variable, String suffix)
        {
            if (variable.GetColumnType().StartsWith("LIST<"))
            {
                if (listDeleteType == ListDeleteType.ALL)
                    sb.Append(variable.GetName() + suffix);
                else
                    sb.Append(variable.GetName() + "[?]" + suffix);
            }
            else
                sb.Append(variable.GetName() + suffix);
        }

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendVariableRows(StringBuilder sb, Column[] variables, String delimiter, String suffix)
        {
            if (variables == null)
                return;

            for (int i = 0; i < variables.Length; i++)
            {
                AppendVariableRow(sb, variables[i], suffix);

                if (i < variables.Length - 1)
                    sb.Append(delimiter + " ");
            }
        }

        public ConsistencyLevel GetConsistencyLevel()
        {
            return consistencyLevel;
        }

        //Om man har ttl så ska den ligga sist i valuesVariables
        //DELETE v1, v2 FROM ks.tb WHERE v1 = ? AND v2 = ? IF EXISTS;
        public String GetString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (tableName == null)
                throw new NullReferenceException("TableName cannot be null");
            if (whereVariables == null)
                throw new NullReferenceException("WhereVariables cannot be null");



            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE ");


            if (variables != null)
            {
                AppendVariableRows(sb, variables, ",", "");
                sb.Append(" ");
            }


            sb.Append("FROM " + keyspace + "." + tableName);

            if (setTimestamp)
                sb.Append(" USING TIMESTAMP ?");

            sb.Append(" WHERE ");

            AppendVariableRows(sb, whereVariables, " AND", " = ?");


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
