using System;
using System.Text;
using System.Collections.Generic;

namespace CassandraQueryBuilder
{
    public class CreateType : Query
    {
        private String keyspace;
        private String table;
        private Column[] colums;

        private List<String> withProperties = new List<string>();

        /// <summary>
        /// To create table queries
        /// </summary>
        public CreateType()
        {

        }

        /// <summary>
        /// Set keyspace name
        /// </summary>
        /// <param name="keyspace">Keyspace name</param>
        /// <returns>CreateType</returns>
        public CreateType Keyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        /// <summary>
        /// Set table name
        /// </summary>
        /// <param name="table">Table name</param>
        /// <returns>CreateType</returns>
        public CreateType Table(String table)
        {
            this.table = table;

            return this;
        }

        /// <summary>
        /// The general columns for the table
        /// </summary>
        /// <param name="columns">The columns in the table</param>
        /// <returns>CreateType</returns>
        public CreateType Columns(params Column[] columns)
        {
            this.colums = columns;

            return this;
        }



        //Returns e.g. "name text, address text, " or "" if null
        private void AppendColumnRows(StringBuilder sb, Column[] column)
        {
            if (column == null)
                return;
            
            for (int i = 0; i < column.Length; i++)
            {
                if(i == column.Length-1)
                    sb.Append(column[i].Name() + " " + column[i].ColumnType());
                else
                    sb.Append(column[i].Name() + " " + column[i].ColumnType() + ", ");
            }
        }


        /// <summary>
        /// Creates the prepared statement string
        /// </summary>
        /// <returns>String</returns>
        public override String ToString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (table == null)
                throw new NullReferenceException("TableName cannot be null");
            if (colums == null)
                throw new NullReferenceException("Columns cannot be null");



            StringBuilder sb = new StringBuilder();

            sb.Append("CREATE TYPE " + keyspace + "." + table + " (");


            AppendColumnRows(sb, colums);


            sb.Append(");");




            return sb.ToString();
        }
        
    }
}
