using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class Select : Query
    {
        private String keyspace;
        private String table;
        private Column[] selectColumns;
        private Column[] selectAsColumns;
        private SelectFunction[] selectFunctions;
        private SelectAggregate[] selectAggregates;
        private Column[] whereColumns;
        private WhereOperator[] whereOperators;
        private int? limit;
        private int?[] inLength;

        /// <summary>
        /// To create SELECT queries
        /// </summary>
        public Select()
        {

        }

        /// <summary>
        /// Set keyspace name
        /// </summary>
        /// <param name="keyspace">Keyspace name</param>
        /// <returns>Select</returns>
        public Select Keyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        /// <summary>
        /// Set table name
        /// </summary>
        /// <param name="table">Table name</param>
        /// <returns>Select</returns>
        public Select Table(String table)
        {
            this.table = table;

            return this;
        }

        /// <summary>
        /// The columns in the SELECT clause
        /// </summary>
        /// <param name="columns">The columns used in the SELECT clause</param>
        /// <returns>Select</returns>
        public Select SelectColumns(params Column[] columns)
        {
            this.selectColumns = columns;

            return this;
        }

        /// <summary>
        /// The as in the SELECT clause
        /// 
        /// SELECT columns_name as other_name etc.
        /// 
        /// Use null for columns not using as
        /// </summary>
        /// <param name="columns">The columns used in the SELECT clause</param>
        /// <returns>Select</returns>
        public Select SelectAs(params Column[] columns)
        {
            this.selectAsColumns = columns;
            
            return this;
        }

        /// <summary>
        /// The functions in the SELECT clause
        /// 
        /// SELECT TTL(...) etc.
        /// 
        /// Use null for columns not using functions
        /// </summary>
        /// <param name="selectFunction">TTL</param>
        /// <returns>Select</returns>
        public Select SelectFunctions(params SelectFunction[] selectFunction)
        {
            this.selectFunctions = selectFunction;
            
            return this;
        }

        /// <summary>
        /// The aggregates in the SELECT clause
        /// 
        /// SELECT COUNT(...), SELECT SUM(...) etc.
        /// 
        /// Use null for columns not using aggregates
        /// </summary>
        /// <param name="selectAggregates">COUNT, SUM, AVG, MAX, MIN</param>
        /// <returns>Select</returns>
        public Select SelectAggregates(params SelectAggregate[] selectAggregates)
        {
            this.selectAggregates = selectAggregates;
            
            return this;
        }

        /// <summary>
        /// The columns used in the WHERE clause
        /// 
        /// Default operator is equals (=)
        /// To change operator, use WhereOperators
        /// </summary>
        /// <param name="whereColumns">The columns used in the WHERE clause</param>
        /// <returns>Select</returns>
        public Select WhereColumns(params Column[] whereColumns)
        {
            this.whereColumns = whereColumns;

            return this;
        }

        /// <summary>
        /// The operators in the WHERE clause
        /// </summary>
        /// <param name="whereOperators">=, <, >, <=, >=, CONTAINS, CONTAINS KEY</param>
        /// <returns>Select</returns>
        public Select WhereOperators(params WhereOperator[] whereOperators)
        {
            this.whereOperators = whereOperators;

            return this;
        }

        //if limit = null here, then it will be "?", otherwise, it will be 1,2,3, or whatever you set
        /// <summary>
        /// To limit the number of results returned
        /// 
        /// If limit = null, then it will be a questionmark (?) to be filled in by the stored procedure.null
        /// If limit != not null, it will be the number you set here
        /// </summary>
        /// <param name="limit">null or a number</param>
        /// <returns>Select</returns>
        public Select Limit(int? limit = null)
        {
            if (limit == null)
                this.limit = 0;
            else
                this.limit = limit;

            return this;
        }

        /// <summary>
        /// For the WHERE part of the clause
        /// 
        /// If null or 0, it will not be included as IN
        /// If not null or 0, it will have as many questionmarks (?) as stated
        /// E.g. SELECT * FROM keyspace.table WHERE name IN (?, ?)
        /// </summary>
        /// <param name="inLength">null, 0 or a number</param>
        /// <returns>Select</returns>
        public Select InColumns(params int?[] inLength)
        {
            this.inLength = inLength;

            return this;
        }

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendSelectColumnRows(StringBuilder sb, Column[] columns, String delimiter, Column[] selectAsColumns, SelectFunction[] selectFunctions, SelectAggregate[] selectAggregates)
        {
            if (columns == null)
                columns = new Column[] {new Column("*", null)};

            for (int i = 0; i < columns.Length; i++)
            {
                if ((selectAggregates == null || selectAggregates.Length == 0 || selectAggregates[i] == null) &&
                    (selectFunctions == null || selectFunctions.Length == 0 || selectFunctions[i] == null))
                    Utils.AppendColumnRow(sb, columns[i]);
                else if(selectAggregates != null && selectAggregates.Length != 0 && selectAggregates[i] != null)
                    Utils.AppendColumnRow(sb, columns[i], selectAggregates[i].Value + "(", ")");
                else
                    Utils.AppendColumnRow(sb, columns[i], selectFunctions[i].Value + "(", ")");

                if(selectAsColumns != null && selectAsColumns.Length != 0 && selectAsColumns[i] != null)
                    sb.Append(" AS " + selectAsColumns[i].Name());
                
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
                    Utils.AppendColumnRow(sb, columns[i], " = ?");
                else
                    Utils.AppendColumnRow(sb, columns[i], " " + whereOperators[i].Value + " ?");

                if (i < columns.Length - 1)
                    sb.Append(delimiter + " ");
            }
        }
        
        /// <summary>
        /// Creates the prepared statement string
        /// 
        /// E.g. SELECT v1, v2 FROM ks.tb WHERE v1 = ? AND v2 = ?;
        /// </summary>
        /// <returns>String</returns>
        public override String ToString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (table == null)
                throw new NullReferenceException("TableName cannot be null");
            if (selectColumns != null && selectAsColumns != null && selectColumns.Length != selectAsColumns.Length)
                throw new IndexOutOfRangeException("SelectColumns and SelectAs must be same length if SelectAs is not null");
            if (selectColumns != null && selectFunctions != null && selectColumns.Length != selectFunctions.Length)
                throw new IndexOutOfRangeException("SelectColumns and SelectFunctions must be same length if SelectFunctions is not null");
            if (selectColumns != null && selectAggregates != null && selectColumns.Length != selectAggregates.Length)
                throw new IndexOutOfRangeException("SelectColumns and SelectAggregates must be same length if SelectAggregates is not null");
            if (whereColumns != null && whereOperators != null && whereColumns.Length != whereOperators.Length)
                throw new IndexOutOfRangeException("WhereColumns and WhereOperators must be same length if WhereOperators is not null");



            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT ");

            AppendSelectColumnRows(sb, selectColumns, ",", selectAsColumns, selectFunctions, selectAggregates);
            

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
