using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class SelectQuery : IQuery// : IPreparedStatement
    {
        private String keyspace;
        private String tableName;
        private Column[] columns;
        private Column[] whereColumns;
        private String[] whereSigns;
        private int? limit;
        private Column inColumn;
        private int inLength;

        //private Object preparedStatmentLock = new Object();
        //private PreparedStatement preparedStatement;
        // private ConsistencyLevel consistencyLevel;

        public SelectQuery()
        {

        }


        public SelectQuery SetKeyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        public SelectQuery SetTableName(String tableName)
        {
            this.tableName = tableName;

            return this;
        }

        public SelectQuery SetColumns(params Column[] columns)
        {
            this.columns = columns;

            return this;
        }

        public SelectQuery SetWhereColumns(params Column[] whereColumns)
        {
            this.whereColumns = whereColumns;

            return this;
        }

        //"=", ">", "<" etc
        public SelectQuery SetWhereSigns(params String[] whereSigns)
        {
            this.whereSigns = whereSigns;

            return this;
        }

        //if limit = null here, then it will be "?", otherwise, it will be 1,2,3, or whatever you set
        public SelectQuery SetLimit(int? limit = null)
        {
            if (limit == null)
                this.limit = 0;
            else
                this.limit = limit;

            return this;
        }

        //if limit = null here, then it will be "?", otherwise, it will be 1,2,3, or whatever you set
        public SelectQuery SetInColumns(Column inColumn, int inLength)
        {
            this.inColumn = inColumn;
            this.inLength = inLength;

            return this;
        }

        //Returns e.g. "name text, " or "name text static, "
        private void AppendColumnRow(StringBuilder sb, Column column, String suffix)
        {
            sb.Append(column.GetName() + suffix);
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

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendColumnRows(StringBuilder sb, Column[] columns, String delimiter, String[] suffix)
        {
            if (columns == null)
                return;

            for (int i = 0; i < columns.Length; i++)
            {
                if (suffix == null || suffix.Length == 0)
                    AppendColumnRow(sb, columns[i], " = ?");
                else
                    AppendColumnRow(sb, columns[i], " " + suffix[i] + " ?");

                if (i < columns.Length - 1)
                    sb.Append(delimiter + " ");
            }
        }
        
        //Om man har ttl så ska den ligga sist i valuesVariables
        //SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v2 = ?;
        public String GetString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (tableName == null)
                throw new NullReferenceException("TableName cannot be null");
            if (whereColumns != null && whereSigns != null && whereSigns.Length != whereSigns.Length)
                throw new IndexOutOfRangeException("whereColumns and whereSigns must be same length if whereSigns is not null");



            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT ");

            if (columns == null)
                sb.Append("*");
            else
                AppendColumnRows(sb, columns, ",", "");
            

            sb.Append(" FROM " + keyspace + "." + tableName);

            if (whereColumns != null && whereColumns.Length > 0)
            {
                sb.Append(" WHERE ");

                AppendColumnRows(sb, whereColumns, " AND", whereSigns);
            }


            if(inColumn != null && inLength > 0)
            {
                if (whereColumns == null)
                    sb.Append(" WHERE ");
                else
                    sb.Append(" AND ");

                sb.Append(inColumn.GetName() + " IN (");

                for (int i = 0; i < inLength; i++)
                {
                    if (i > 0)
                        sb.Append(", ");
                    sb.Append("?");
                }

                sb.Append(")");
            }


            if (limit != null)
                sb.Append(" LIMIT " + (limit == 0 ? "?" : limit.ToString()));



            sb.Append(";");

            

            return sb.ToString();
        }


        /*public async Task<PreparedStatement> GetPreparedStatement()
        {
            if (preparedStatement != null)
                return preparedStatement;

            await Task.Run(() => PrepareStatement());

            return preparedStatement;
        }

        private void PrepareStatement()
        {
            lock (preparedStatmentLock)
            {
                //To make sure that if multiple threads are calling this at the same time. If two have passed the if statement above, then the second one getting into this will just return the prepared statement and not create another one
                if (preparedStatement != null)
                    return;

                //Using the p below since in the GetPraparedStatement, the preparedStatement should not be set before by first setting it and then afterwards setting the consistency level
                PreparedStatement p = DB.CreatePreparedStatement(GetString());

                if (consistencyLevel != null)
                    p.SetConsistencyLevel(consistencyLevel.Value);

                preparedStatement = p;

            }
        }*/


    }
}
