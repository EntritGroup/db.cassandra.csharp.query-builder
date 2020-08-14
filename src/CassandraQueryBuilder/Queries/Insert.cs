using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class Insert : Query
    {
        private String keyspace;
        private String table;
        private Column[] insertColumns;
        private Boolean ttl = false;
        private Boolean ifNotExists = false;
        private Boolean setTimestamp = false;

        /// <summary>
        /// To create INSERT queries
        /// </summary>
        public Insert()
        {

        }

        /// <summary>
        /// Set keyspace name
        /// </summary>
        /// <param name="keyspace">Keyspace name</param>
        /// <returns>Insert</returns>
        public Insert Keyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        /// <summary>
        /// Set table name
        /// </summary>
        /// <param name="table">Table name</param>
        /// <returns>Insert</returns>
        public Insert Table(String table)
        {
            this.table = table;

            return this;
        }

        /// <summary>
        /// The columns in the INSERT clause
        /// </summary>
        /// <param name="insertColumns">The columns used in the INSERT clause</param>
        /// <returns>Insert</returns>
        public Insert InsertColumns(params Column[] insertColumns)
        {
            this.insertColumns = insertColumns;

            return this;
        }

        /// <summary>
        /// Set Time To Live (TTL) in the INSERT clause
        /// </summary>
        /// <returns>Insert</returns>
        public Insert TTL()
        {
            this.ttl = true;

            return this;
        }

        /// <summary>
        /// Set IF NOT EXISTS in the INSERT clause
        /// </summary>
        /// <returns>Insert</returns>
        public Insert IfNotExists()
        {
            this.ifNotExists = true;

            return this;
        }

        /// <summary>
        /// Set insert timestamp
        /// </summary>
        /// <returns>Insert</returns>
        public Insert Timestamp()
        {
            this.setTimestamp = true;

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
        
        /// <summary>
        /// Creates the prepared statement string
        /// 
        /// E.g. INSERT INTO ks.tb (v1, v2) VALUES (?, ?) IF NOT EXISTS USING TTL ?;
        /// </summary>
        /// <returns>String</returns>
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
