using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class Delete : Query// : IPreparedStatement
    {
        private String keyspace;
        private String tableName;
        private Column[] variables;
        private Column[] whereVariables;
        private Boolean ifExists = false;
        private Boolean setTimestamp = false;

        //private Object preparedStatmentLock = new Object();
        //private PreparedStatement preparedStatement;
        // private ConsistencyLevel consistencyLevel;

        private ListDeleteType listDeleteType;

        public Delete()
        {

        }


        public Delete SetKeyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        public Delete SetTableName(String tableName)
        {
            this.tableName = tableName;

            return this;
        }

        public Delete SetVariables(params Column[] variables)
        {
            this.variables = variables;

            return this;
        }

        public Delete SetWhereVariables(params Column[] whereVariables)
        {
            this.whereVariables = whereVariables;

            return this;
        }

        public Delete SetIfExists()
        {
            this.ifExists = true;

            return this;
        }

        public Delete SetTimestamp()
        {
            this.setTimestamp= true;

            return this;
        }

        public Delete SetListDeleteType(ListDeleteType listDeleteType)
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

        //Om man har ttl så ska den ligga sist i valuesVariables
        //DELETE v1, v2 FROM ks.tb WHERE v1 = ? AND v2 = ? IF EXISTS;
        public override String ToString()
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

    }
}
