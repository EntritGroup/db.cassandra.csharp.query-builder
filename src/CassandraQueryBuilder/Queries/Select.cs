using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class Select : Query// : IPreparedStatement
    {
        private String keyspace;
        private String table;
        private Column[] selectColumns;
        private Column[] whereColumns;
        private WhereOperator[] whereOperators;
        private int? limit;
        private Column inColumn;
        private int inLength;

        //private Object preparedStatmentLock = new Object();
        //private PreparedStatement preparedStatement;
        // private ConsistencyLevel consistencyLevel;

        public Select()
        {

        }


        public Select Keyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        public Select Table(String table)
        {
            this.table = table;

            return this;
        }

        public Select SelectColumns(params Column[] columns)
        {
            this.selectColumns = columns;

            return this;
        }

        public Select WhereColumns(params Column[] whereColumns)
        {
            this.whereColumns = whereColumns;

            return this;
        }

        //= | < | > | <= | >= | CONTAINS | CONTAINS KEY
        public Select WhereOperators(params WhereOperator[] whereOperators)
        {
            this.whereOperators = whereOperators;

            return this;
        }

        //if limit = null here, then it will be "?", otherwise, it will be 1,2,3, or whatever you set
        public Select Limit(int? limit = null)
        {
            if (limit == null)
                this.limit = 0;
            else
                this.limit = limit;

            return this;
        }

        //if limit = null here, then it will be "?", otherwise, it will be 1,2,3, or whatever you set
        public Select InColumns(Column inColumn, int inLength)
        {
            this.inColumn = inColumn;
            this.inLength = inLength;

            return this;
        }

        //Returns e.g. "name text, " or "name text static, "
        private void AppendColumnRow(StringBuilder sb, Column column)
        {
            sb.Append(column.Name());
        }

        //Returns e.g. "name text, " or "name text static, "
        private void AppendColumnRow(StringBuilder sb, Column column, String suffix)
        {
            sb.Append(column.Name() + suffix);
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

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendColumnRows(StringBuilder sb, Column[] columns, String delimiter, WhereOperator[] whereOperators)
        {
            if (columns == null)
                return;

            for (int i = 0; i < columns.Length; i++)
            {
                if (whereOperators == null || whereOperators.Length == 0)
                    AppendColumnRow(sb, columns[i], " = ?");
                else
                    AppendColumnRow(sb, columns[i], " " + whereOperators[i].Value + " ?");

                if (i < columns.Length - 1)
                    sb.Append(delimiter + " ");
            }
        }
        
        //Om man har ttl så ska den ligga sist i valuesVariables
        //SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v2 = ?;
        public override String ToString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (table == null)
                throw new NullReferenceException("TableName cannot be null");
            if (whereColumns != null && whereOperators != null && whereOperators.Length != whereOperators.Length)
                throw new IndexOutOfRangeException("whereColumns and whereSigns must be same length if whereSigns is not null");



            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT ");

            if (selectColumns == null)
                sb.Append("*");
            else
                AppendColumnRows(sb, selectColumns, ",");
            

            sb.Append(" FROM " + keyspace + "." + table);

            if (whereColumns != null && whereColumns.Length > 0)
            {
                sb.Append(" WHERE ");

                AppendColumnRows(sb, whereColumns, " AND", whereOperators);
            }


            if(inColumn != null && inLength > 0)
            {
                if (whereColumns == null)
                    sb.Append(" WHERE ");
                else
                    sb.Append(" AND ");

                sb.Append(inColumn.Name() + " IN (");

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

    }
}
