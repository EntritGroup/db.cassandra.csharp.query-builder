using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class Delete : Query
    {
        private String keyspace;
        private String table;
        private Column[] deleteColumns;
        private Column[] whereColumns;
        private Boolean ifExists = false;
        private Boolean setTimestamp = false;

        private MapDeleteType[] mapDeleteTypes;
        private ListDeleteType[] listDeleteTypes;

        private int mapUpdateTypesCounter = 0;
        private int listUpdateTypesCounter = 0;

        /// <summary>
        /// To create DELETE queries
        /// </summary>
        public Delete()
        {

        }


        /// <summary>
        /// Set keyspace name
        /// </summary>
        /// <param name="keyspace">Keyspace name</param>
        /// <returns>Delete</returns>
        public Delete Keyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        /// <summary>
        /// Set table name
        /// </summary>
        /// <param name="table">Table name</param>
        /// <returns>Delete</returns>
        public Delete Table(String table)
        {
            this.table = table;

            return this;
        }

        /// <summary>
        /// The columns in the DELETE clause
        /// </summary>
        /// <param name="deleteColumns">The columns used in the DELETE clause</param>
        /// <returns>Delete</returns>
        public Delete DeleteColumns(params Column[] deleteColumns)
        {
            this.deleteColumns = deleteColumns;

            return this;
        }

        /// <summary>
        /// The columns used in the WHERE clause
        /// </summary>
        /// <param name="whereColumns">The columns used in the WHERE clause</param>
        /// <returns>Delete</returns>
        public Delete WhereColumns(params Column[] whereColumns)
        {
            this.whereColumns = whereColumns;

            return this;
        }

        /// <summary>
        /// Add IF EXISTS to the query
        /// </summary>
        /// <returns>Delete</returns>
        public Delete IfExists()
        {
            this.ifExists = true;

            return this;
        }

        /// <summary>
        /// Add timestamp to the query
        /// </summary>
        /// <returns>Delete</returns>
        public Delete Timestamp()
        {
            this.setTimestamp= true;

            return this;
        }

        /// <summary>
        /// Set delete type for LIST
        /// </summary>
        /// <param name="listDeleteTypes">SELECTED, ALL</param>
        /// <returns>Delete</returns>
        public Delete ListDeleteType(params ListDeleteType[] listDeleteTypes)
        {
            this.listDeleteTypes = listDeleteTypes;

            return this;
        }
        
        /// <summary>
        /// Set delete type for MAP
        /// </summary>
        /// <param name="mapDeleteTypes">SELECTED, ALL</param>
        /// <returns>Delete</returns>
        public Delete MapDeleteType(params MapDeleteType[] mapDeleteTypes)
        {
            this.mapDeleteTypes = mapDeleteTypes;

            return this;
        }



        //Returns e.g. "name text, " or "name text static, "
        //https://docs.datastax.com/en/dse/6.7/cql/cql/cql_reference/cql_commands/cqlDelete.html
        private void AppendColumnRow(StringBuilder sb, Column column, String suffix)
        {
            if (column.ColumnType().StartsWith("MAP<"))
            {
                if (mapDeleteTypes[mapUpdateTypesCounter] == CassandraQueryBuilder.MapDeleteType.ALL)
                    sb.Append(column.Name() + suffix);
                else //MapDeleteType.SELECTED
                    sb.Append(column.Name() + "[?]" + suffix);

                mapUpdateTypesCounter++;
            }
            else if (column.ColumnType().StartsWith("LIST<"))
            {
                if (listDeleteTypes[listUpdateTypesCounter] == CassandraQueryBuilder.ListDeleteType.ALL)
                    sb.Append(column.Name() + suffix);
                else //ListDeleteType.SELECTED
                    sb.Append(column.Name() + "[?]" + suffix);

                listUpdateTypesCounter++;
            }
            else
                sb.Append(column.Name() + suffix);
        }

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendColumnRows(StringBuilder sb, Column[] columns, String delimiter, String suffix)
        {
            if (columns == null)
                return;

            for (int i = 0; i < columns.Length; i++)
            {
                AppendColumnRow(sb, columns[i], suffix);

                if (i < columns.Length - 1)
                    sb.Append(delimiter + " ");
            }
        }

        /// <summary>
        /// Creates the prepared statement string
        /// 
        /// E.g. DELETE v1, v2 FROM ks.tb WHERE v1 = ? AND v2 = ? IF EXISTS;
        /// </summary>
        /// <returns>String</returns>
        public override String ToString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (table == null)
                throw new NullReferenceException("TableName cannot be null");
            if (whereColumns == null)
                throw new NullReferenceException("WhereColumns cannot be null");



            StringBuilder sb = new StringBuilder();

            sb.Append("DELETE ");


            if (deleteColumns != null)
            {
                AppendColumnRows(sb, deleteColumns, ",", "");
                sb.Append(" ");
            }


            sb.Append("FROM " + keyspace + "." + table);

            if (setTimestamp)
                sb.Append(" USING TIMESTAMP ?");

            sb.Append(" WHERE ");

            AppendColumnRows(sb, whereColumns, " AND", " = ?");


            if (ifExists)
                sb.Append(" IF EXISTS");


            sb.Append(";");

            

            return sb.ToString();
        }

    }
}
