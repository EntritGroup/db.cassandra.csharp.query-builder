using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class Insert : Query// : IPreparedStatement
    {
        private String keyspace;
        private String table;
        private Column[] insertColumns;
        private Boolean ttl = false; //Om man har ttl så ska den ligga sist i valuesVariables
        private Boolean ifNotExists = false;
        private Boolean setTimestamp = false;

        //private Object preparedStatmentLock = new Object();
        //private PreparedStatement preparedStatement;
        // private ConsistencyLevel consistencyLevel;

        public Insert()
        {

        }


        public Insert Keyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        public Insert Table(String table)
        {
            this.table = table;

            return this;
        }

        public Insert InsertColumns(params Column[] variables)
        {
            this.insertColumns = variables;

            return this;
        }

        //Om man har ttl så ska den ligga sist i valuesVariables
        public Insert TTL()
        {
            this.ttl = true;

            return this;
        }

        public Insert IfNotExists()
        {
            this.ifNotExists = true;

            return this;
        }

        public Insert Timestamp()
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
        public override String ToString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (table == null)
                throw new NullReferenceException("TableName cannot be null");
            if (insertColumns == null)
                throw new NullReferenceException("Variables cannot be null");



            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO " + keyspace + "." + table + " (");

            
            AppendVariableRows(sb, insertColumns);


            sb.Append(") VALUES (");


            for (int i = 0; i < insertColumns.Length; i++)
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

    }
}
