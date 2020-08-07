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

        private MapDeleteType[] mapDeleteTypes;
        private ListDeleteType[] listDeleteTypes;

        private int mapUpdateTypesCounter = 0;
        private int listUpdateTypesCounter = 0;


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

        public Delete ListDeleteType(params ListDeleteType[] listDeleteTypes)
        {
            this.listDeleteTypes = listDeleteTypes;

            return this;
        }
        
        public Delete MapDeleteType(params MapDeleteType[] mapDeleteTypes)
        {
            this.mapDeleteTypes = mapDeleteTypes;

            return this;
        }



        //Returns e.g. "name text, " or "name text static, "
        //https://docs.datastax.com/en/dse/6.7/cql/cql/cql_reference/cql_commands/cqlDelete.html
        private void AppendVariableRow(StringBuilder sb, Column column, String suffix)
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
