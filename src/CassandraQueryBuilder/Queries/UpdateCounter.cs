using System;
using System.Text;

namespace CassandraQueryBuilder
{
    public class UpdateCounter : Query// : IPreparedStatement
    {
        private String keyspace;
        private String table;
        private Column column;
        private Column[] whereColumns;
        private int? increaseBy; //increaseBy = Increase or decrease by (e.g. 1, 2, -1, -5)

        //private Object preparedStatmentLock = new Object();
        //private PreparedStatement preparedStatement;
        // private ConsistencyLevel consistencyLevel;

        public UpdateCounter()
        {

        }


        public UpdateCounter Keyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        public UpdateCounter Table(String table)
        {
            this.table = table;

            return this;
        }

        public UpdateCounter UpdateColumn(Column column)
        {
            this.column = column;

            return this;
        }

        public UpdateCounter WhereColumns(params Column[] whereColumns)
        {
            this.whereColumns = whereColumns;

            return this;
        }

        //increaseBy = Increase or decrease by (e.g. 1, 2, -1, -5)
        public UpdateCounter IncreaseBy(int increaseBy)
        {
            this.increaseBy = increaseBy;

            return this;
        }

        //Returns e.g. "name text, " or "name text static, "
        private void AppendVariableRow(StringBuilder sb, Column variable)
        {
            sb.Append(variable.GetName() + " = ?");
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
        //Om man har ttl så ska den ligga sist i valuesVariables
        //UPDATE ks.tb SET counter_column_name = counter_column_name + -1 WHERE pk1 = ? AND pk2 = ?;
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


            
            sb.Append(column.GetName() + " = " + column.GetName() + " + " + (increaseBy == null ? "?" : increaseBy.ToString()));


            sb.Append(" WHERE ");


            AppendVariableRows(sb, whereColumns, " AND");
            
            
            sb.Append(";");

            

            return sb.ToString();
        }

    }
}
