using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class UpdateCounter : Query
    {
        private String keyspace;
        private String table;
        private Column column;
        private Column[] whereColumns;
        private int? increaseBy; //increaseBy = Increase or decrease by (e.g. 1, 2, -1, -5)

        /// <summary>
        /// To create UPDATE queries for counters
        /// </summary>
        public UpdateCounter()
        {

        }

        /// <summary>
        /// Set keyspace name
        /// </summary>
        /// <param name="keyspace">Keyspace name</param>
        /// <returns>UpdateCounter</returns>
        public UpdateCounter Keyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        /// <summary>
        /// Set table name
        /// </summary>
        /// <param name="table">Table name</param>
        /// <returns>UpdateCounter</returns>
        public UpdateCounter Table(String table)
        {
            this.table = table;

            return this;
        }

        /// <summary>
        /// The columns in the UPDATE clause
        /// </summary>
        /// <param name="columns">The columns used in the UPDATE clause</param>
        /// <returns>UpdateCounter</returns>
        public UpdateCounter UpdateColumn(Column column)
        {
            this.column = column;

            return this;
        }

        /// <summary>
        /// The columns used in the WHERE clause
        /// </summary>
        /// <param name="whereColumns">The columns used in the WHERE clause</param>
        /// <returns>UpdateCounter</returns>
        public UpdateCounter WhereColumns(params Column[] whereColumns)
        {
            this.whereColumns = whereColumns;

            return this;
        }

        /// <summary>
        /// Increase or decrease in the counter
        /// </summary>
        /// <param name="increaseBy">E.g. 1, 2, -1, -5</param>
        /// <returns>UpdateCounter</returns>
        public UpdateCounter IncreaseBy(int increaseBy)
        {
            this.increaseBy = increaseBy;

            return this;
        }

        //Returns e.g. "name text, " or "name text static, "
        private void AppendVariableRow(StringBuilder sb, Column variable)
        {
            sb.Append(variable.Name() + " = ?");
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
        
        //increaseBy = Increase or decrease by (e.g. 1, 2, -1, -5)
        /// <summary>
        /// Creates the prepared statement string
        /// 
        /// E.g. UPDATE ks.tb SET counter_column_name = counter_column_name + -1 WHERE pk1 = ? AND pk2 = ?;
        /// </summary>
        /// <returns>String</returns>
        public override String ToString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (table == null)
                throw new NullReferenceException("TableName cannot be null");
            if (column == null)
                throw new NullReferenceException("Variables cannot be null");
            if (whereColumns == null)
                throw new NullReferenceException("WhereVariables cannot be null");



            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE " + keyspace + "." + table + " SET ");


            
            sb.Append(column.Name() + " = " + column.Name() + " + " + (increaseBy == null ? "?" : increaseBy.ToString()));


            sb.Append(" WHERE ");


            AppendVariableRows(sb, whereColumns, " AND");
            
            
            sb.Append(";");

            

            return sb.ToString();
        }

    }
}
