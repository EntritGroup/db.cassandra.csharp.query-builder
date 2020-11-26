using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class UpdateCounter : Query
    {
        private String keyspace;
        private String table;
        private Column[] updateColumns;
        private Column[] whereColumns;
        private int?[] increaseBy; //increaseBy = Increase or decrease by (e.g. 1, 2, -1, -5)

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
        /// <param name="column">The columns used in the UPDATE clause</param>
        /// <returns>UpdateCounter</returns>
        public UpdateCounter UpdateColumns(params Column[] updateColumns)
        {
            this.updateColumns = updateColumns;

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
        /// Increase or decrease the counters
        /// </summary>
        /// <param name="increaseBy">E.g. 1, 2, -1, -5, null (for ?)</param>
        /// <returns>UpdateCounter</returns>
        public UpdateCounter IncreaseBy(params int?[] increaseBy)
        {
            this.increaseBy = increaseBy;

            return this;
        }

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendColumnRows(StringBuilder sb, Column[] columns, String delimiter)
        {
            if (columns == null)
                return;

            for (int i = 0; i < columns.Length; i++)
            {
                Utils.AppendColumnRow(sb, columns[i], " = ?");

                if (i < columns.Length - 1)
                    sb.Append(delimiter + " ");
            }
        }
        
        //increaseBy = Increase or decrease by (e.g. 1, 2, -1, -5)
        /// <summary>
        /// Creates the prepared statement string
        /// 
        /// E.g. UPDATE ks.tb SET c1 = c1 + -1 WHERE pk1 = ? AND pk2 = ?;
        /// </summary>
        /// <returns>String</returns>
        public override String ToString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (table == null)
                throw new NullReferenceException("TableName cannot be null");
            if (updateColumns == null)
                throw new NullReferenceException("Columns cannot be null");
            if (whereColumns == null)
                throw new NullReferenceException("WhereColumns cannot be null");
            if (increaseBy != null && updateColumns.Length != increaseBy.Length) //updateColumns != null is already checked above
                throw new IndexOutOfRangeException("UpdateColumns and IncreasyBy must be same length if increaseBy is not null");
            



            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE " + keyspace + "." + table + " SET ");


            for (int i = 0; i < updateColumns.Length; i++)
            {
                if (increaseBy == null || increaseBy[i] == null || increaseBy.Length == 0)
                    sb.Append(updateColumns[i].Name() + " = " + updateColumns[i].Name() + " + " + "?");
                else
                    sb.Append(updateColumns[i].Name() + " = " + updateColumns[i].Name() + " + " + increaseBy[i].ToString());

                if (i < updateColumns.Length - 1)
                    sb.Append(", ");
            }


            sb.Append(" WHERE ");


            AppendColumnRows(sb, whereColumns, " AND");
            
            
            sb.Append(";");

            

            return sb.ToString();
        }

    }
}
