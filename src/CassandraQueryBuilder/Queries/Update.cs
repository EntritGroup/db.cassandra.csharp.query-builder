using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class Update : Query// : IPreparedStatement
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
        // private ConsistencyLevel consistencyLevel;

        private ListUpdateType listUpdateType;

        public Update()
        {

        }


        public Update SetKeyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        public Update SetTableName(String tableName)
        {
            this.tableName = tableName;

            return this;
        }

        public Update SetVariables(params Column[] variables)
        {
            this.variables = variables;

            return this;
        }

        public Update SetWhereVariables(params Column[] whereVariables)
        {
            this.whereVariables = whereVariables;

            return this;
        }

        //Om man har ttl så ska den ligga sist i valuesVariables
        public Update SetTTL()
        {
            this.ttl = true;

            return this;
        }

        public Update SetIfExists()
        {
            this.ifExists = true;

            return this;
        }

        public Update SetTimestamp()
        {
            this.setTimestamp = true;

            return this;
        }

        public Update SetListUpdateType(ListUpdateType listUpdateType)
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
        public override String ToString()
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

    }
}
