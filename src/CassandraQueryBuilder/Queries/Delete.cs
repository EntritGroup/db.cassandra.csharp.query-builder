using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class Delete : Query// : IPreparedStatement
    {
        private String keyspace;
        private String table;
        private Column[] deleteColumns;
        private Column[] whereColumns;
        private Boolean ifExists = false;
        private Boolean setTimestamp = false;

        //private Object preparedStatmentLock = new Object();
        //private PreparedStatement preparedStatement;
        // private ConsistencyLevel consistencyLevel;

        private ListDeleteType listDeleteType;

        public Delete()
        {

        }


        public Delete Keyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        public Delete Table(String table)
        {
            this.table = table;

            return this;
        }

        public Delete DeleteColumns(params Column[] deleteColumns)
        {
            this.deleteColumns = deleteColumns;

            return this;
        }

        public Delete WhereColumns(params Column[] whereColumns)
        {
            this.whereColumns = whereColumns;

            return this;
        }

        public Delete IfExists()
        {
            this.ifExists = true;

            return this;
        }

        public Delete Timestamp()
        {
            this.setTimestamp= true;

            return this;
        }

        public Delete ListDeleteType(ListDeleteType listDeleteType)
        {
            this.listDeleteType = listDeleteType;

            return this;
        }



        //Returns e.g. "name text, " or "name text static, "
        private void AppendVariableRow(StringBuilder sb, Column variable, String suffix)
        {
            if (variable.GetColumnType().StartsWith("LIST<"))
            {
                if (listDeleteType == CassandraQueryBuilder.ListDeleteType.ALL)
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
            if (table == null)
                throw new NullReferenceException("TableName cannot be null");
            if (whereColumns == null)
                throw new NullReferenceException("WhereVariables cannot be null");



            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE ");


            if (deleteColumns != null)
            {
                AppendVariableRows(sb, deleteColumns, ",", "");
                sb.Append(" ");
            }


            sb.Append("FROM " + keyspace + "." + table);

            if (setTimestamp)
                sb.Append(" USING TIMESTAMP ?");

            sb.Append(" WHERE ");

            AppendVariableRows(sb, whereColumns, " AND", " = ?");


            if (ifExists)
                sb.Append(" IF EXISTS");


            sb.Append(";");

            

            return sb.ToString();
        }

    }
}
