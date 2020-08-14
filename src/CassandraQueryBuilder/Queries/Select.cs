using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class Select : Query// : IPreparedStatement
    {
        private String keyspace;
        private String table;
        private Column[] selectColumns;
        private SelectAggregate[] selectAggregates;
        private Column[] whereColumns;
        private WhereOperator[] whereOperators;
        private int? limit;
        private int?[] inLength;

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

        //COUNT, SUM, AVG, MAX, MIN
        public Select SelectAggregates(params SelectAggregate[] selectAggregates)
        {
            this.selectAggregates = selectAggregates;

            return this;
        }

        public Select WhereColumns(params Column[] whereColumns)
        {
            this.whereColumns = whereColumns;

            return this;
        }

        //=, <, >, <=, >=, CONTAINS, CONTAINS KEY
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

        //for the In clause (where X in Y)
        public Select InColumns(params int?[] inLength)
        {
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

        //Returns e.g. "name text, " or "name text static, "
        private void AppendColumnRow(StringBuilder sb, Column column, String prefix, String suffix)
        {
            sb.Append(prefix + column.Name() + suffix);
        }

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendSelectColumnRows(StringBuilder sb, Column[] columns, String delimiter, SelectAggregate[] selectAggregates)
        {
            if (columns == null)
                columns = new Column[] {new Column("*", null)};

            for (int i = 0; i < columns.Length; i++)
            {
                if (selectAggregates == null || selectAggregates[i] == null || selectAggregates.Length == 0 || selectAggregates[i] == null)
                    AppendColumnRow(sb, columns[i]);
                else
                    AppendColumnRow(sb, columns[i], selectAggregates[i].Value + "(", ")");

                if (i < columns.Length - 1)
                    sb.Append(delimiter + " ");
            }
        }

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendWhereColumnRows(StringBuilder sb, Column[] columns, String delimiter, WhereOperator[] whereOperators)
        {
            if (columns == null)
                return;

            for (int i = 0; i < columns.Length; i++)
            {
                if(inLength != null && inLength[i] != null && inLength[i] != 0)
                {
                    sb.Append(columns[i].Name() + " IN (");

                    for (int j = 0; j < inLength[i]; j++)
                    {
                        if (j > 0)
                            sb.Append(", ");
                        sb.Append("?");
                    }

                    sb.Append(")");
                }
                else if (whereOperators == null || whereOperators[i] == null || whereOperators.Length == 0 || whereOperators[i] == null)
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
            if (selectColumns != null && selectAggregates != null && selectColumns.Length != selectAggregates.Length)
                throw new IndexOutOfRangeException("WhereColumns and WhereOperators must be same length if WhereOperators is not null");
            if (whereColumns != null && whereOperators != null && whereColumns.Length != whereOperators.Length)
                throw new IndexOutOfRangeException("WhereColumns and WhereOperators must be same length if WhereOperators is not null");



            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT ");

            AppendSelectColumnRows(sb, selectColumns, ",", selectAggregates);
            

            sb.Append(" FROM " + keyspace + "." + table);

            if (whereColumns != null && whereColumns.Length > 0)
            {
                sb.Append(" WHERE ");

                AppendWhereColumnRows(sb, whereColumns, " AND", whereOperators);
            }


            if (limit != null)
                sb.Append(" LIMIT " + (limit == 0 ? "?" : limit.ToString()));



            sb.Append(";");

            

            return sb.ToString();
        }

    }
}
