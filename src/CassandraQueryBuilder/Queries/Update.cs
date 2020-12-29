using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class Update : Query
    {
        private String keyspace;
        private String table;
        private Column[] updateColumns;
        private Column[] whereColumns;
        private Boolean ttl = false;
        private Boolean ifExists = false;
        private Boolean setTimestamp = false;

        private MapUpdateType[] mapUpdateTypes;
        private SetUpdateType[] setUpdateTypes;
        private ListUpdateType[] listUpdateTypes;

        private int mapUpdateTypesCounter = 0;
        private int setUpdateTypesCounter = 0;
        private int listUpdateTypesCounter = 0;

        /// <summary>
        /// To create UPDATE queries
        /// </summary>
        public Update()
        {

        }

        /// <summary>
        /// Set keyspace name
        /// </summary>
        /// <param name="keyspace">Keyspace name</param>
        /// <returns>Update</returns>
        public Update Keyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        /// <summary>
        /// Set table name
        /// </summary>
        /// <param name="table">Table name</param>
        /// <returns>Update</returns>
        public Update Table(String table)
        {
            this.table = table;

            return this;
        }

        /// <summary>
        /// The columns in the UPDATE clause
        /// </summary>
        /// <param name="updateColumns">The columns used in the UPDATE clause</param>
        /// <returns>Update</returns>
        public Update UpdateColumns(params Column[] updateColumns)
        {
            this.updateColumns = updateColumns;

            return this;
        }

        /// <summary>
        /// The columns used in the WHERE clause
        /// </summary>
        /// <param name="whereColumns">The columns used in the WHERE clause</param>
        /// <returns>Update</returns>
        public Update WhereColumns(params Column[] whereColumns)
        {
            this.whereColumns = whereColumns;

            return this;
        }

        /// <summary>
        /// Set Time To Live (TTL) in the UPDATE clause
        /// 
        /// E.g. UPDATE ks.tb USING TTL ? SET v2 = ? WHERE v1 = ?
        /// </summary>
        /// <returns>Update</returns>
        public Update TTL()
        {
            this.ttl = true;

            return this;
        }

        /// <summary>
        /// Set IF EXISTS in the UPDATE clause
        /// </summary>
        /// <returns>Update</returns>
        public Update IfExists()
        {
            this.ifExists = true;

            return this;
        }

        /// <summary>
        /// Set update timestamp
        /// 
        /// /// E.g. UPDATE ks.tb USING TIMESTAMP ? SET v2 = ? WHERE v1 = ?;";
        /// </summary>
        /// <returns>Update</returns>
        public Update Timestamp()
        {
            this.setTimestamp = true;

            return this;
        }

        /// <summary>
        /// Set update type for MAP
        /// </summary>
        /// <param name="mapUpdateTypes">ADD, REMOVE</param>
        /// <returns>Update</returns>
        public Update MapUpdateTypes(params MapUpdateType[] mapUpdateTypes)
        {
            this.mapUpdateTypes = mapUpdateTypes;

            return this;
        }
        
        /// <summary>
        /// Set update type for SET
        /// </summary>
        /// <param name="setUpdateTypes">ADD, REMOVE</param>
        /// <returns>Update</returns>
        public Update SetUpdateTypes(params SetUpdateType[] setUpdateTypes)
        {
            this.setUpdateTypes = setUpdateTypes;

            return this;
        }
        
        /// <summary>
        /// Set update type for LIST
        /// </summary>
        /// <param name="listUpdateTypes">PREPEND, APPEND, REPLACE_ALL, SPECIFY_INDEX_TO_OVERWRITE</param>
        /// <returns>Update</returns>
        public Update ListUpdateTypes(params ListUpdateType[] listUpdateTypes)
        {
            this.listUpdateTypes = listUpdateTypes;

            return this;
        }





        //Returns e.g. "name text, " or "name text static, "
        private void AppendColumnRow(StringBuilder sb, Column column)
        {
            if(column.ColumnType().StartsWith("MAP<"))
            {
                if (mapUpdateTypes[mapUpdateTypesCounter] == CassandraQueryBuilder.MapUpdateType.ADD)
                    sb.Append(column.Name() + " = " + column.Name() + " + ?");
                else //SetUpdateType.Remove
                    sb.Append(column.Name() + " = " + column.Name() + " - ?");
                
                mapUpdateTypesCounter++;
            }
            else if(column.ColumnType().StartsWith("SET<"))
            {
                if (setUpdateTypes[setUpdateTypesCounter] == CassandraQueryBuilder.SetUpdateType.ADD)
                    sb.Append(column.Name() + " = " + column.Name() + " + ?");
                else //SetUpdateType.Remove
                    sb.Append(column.Name() + " = " + column.Name() + " - ?");

                setUpdateTypesCounter++;
            }
            else if(column.ColumnType().StartsWith("LIST<"))
            {
                if (listUpdateTypes[listUpdateTypesCounter] == CassandraQueryBuilder.ListUpdateType.PREPEND)
                    sb.Append(column.Name() + " = ? + " + column.Name());
                else if (listUpdateTypes[listUpdateTypesCounter] == CassandraQueryBuilder.ListUpdateType.APPEND)
                    sb.Append(column.Name() + " = " + column.Name() + " + ?");
                else if (listUpdateTypes[listUpdateTypesCounter] == CassandraQueryBuilder.ListUpdateType.REPLACE_ALL)
                    sb.Append(column.Name() + " = ?");
                else //ListUpdateType.SPECIFY_INDEX_TO_OVERWRITE
                    sb.Append(column.Name()+ "[?] = ?");

                listUpdateTypesCounter++;
            }
            else
                sb.Append(column.Name() + " = ?");
        }

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendColumnRows(StringBuilder sb, Column[] columns, String delimiter)
        {
            if (columns == null)
                return;

            for (int i = 0; i < columns.Length; i++)
            {
                AppendColumnRow(sb, columns[i]);

                if (i < columns.Length - 1)
                    sb.Append(delimiter + " ");
            }
        }

        /// <summary>
        /// Creates the prepared statement string
        /// 
        /// E.g. UPDATE ks.tb SET v1 = ?, v2 = ? WHERE v1 = ? AND v2 = ? IF EXISTS;
        /// </summary>
        /// <returns>String</returns>
        public override String ToString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (table == null)
                throw new NullReferenceException("TableName cannot be null");
            if (updateColumns == null && !ttl) //You can update the TTL for whole partition without specifying columns
                throw new NullReferenceException("UpdateColumns cannot be null");
            if (whereColumns == null)
                throw new NullReferenceException("WhereColumns cannot be null");



            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE " + keyspace + "." + table);

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

            AppendColumnRows(sb, updateColumns, ",");


            sb.Append(" WHERE ");


            AppendColumnRows(sb, whereColumns, " AND");
            

            if (ifExists)
                sb.Append(" IF EXISTS");


            sb.Append(";");

            

            return sb.ToString();
        }

    }
}
