using System;
using System.Text;
using System.Collections.Generic;

namespace CassandraQueryBuilder
{
    public class Update : Query// : IPreparedStatement
    {
        private String keyspace;
        private String table;
        private Column[] updateColumns;
        private Column[] whereColumns;
        private Boolean ttl = false; //Om man har ttl så ska den ligga sist i valuesVariables;
        private Boolean ifExists = false;
        private Boolean setTimestamp = false;

        //private Object preparedStatmentLock = new Object();
        //private PreparedStatement preparedStatement;
        // private ConsistencyLevel consistencyLevel;

        private MapUpdateType[] mapUpdateTypes;
        private SetUpdateType[] setUpdateTypes;
        private ListUpdateType[] listUpdateTypes;

        private int mapUpdateTypesCounter = 0;
        private int setUpdateTypesCounter = 0;
        private int listUpdateTypesCounter = 0;

        public Update()
        {

        }


        public Update Keyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        public Update Table(String table)
        {
            this.table = table;

            return this;
        }

        public Update UpdateColumns(params Column[] updateColumns)
        {
            this.updateColumns = updateColumns;

            return this;
        }

        public Update WhereColumns(params Column[] whereColumns)
        {
            this.whereColumns = whereColumns;

            return this;
        }

        //Om man har ttl så ska den ligga sist i valuesVariables
        public Update TTL()
        {
            this.ttl = true;

            return this;
        }

        public Update IfExists()
        {
            this.ifExists = true;

            return this;
        }

        public Update Timestamp()
        {
            this.setTimestamp = true;

            return this;
        }

        public Update MapUpdateType(params MapUpdateType[] mapUpdateTypes)
        {
            this.mapUpdateTypes = mapUpdateTypes;

            return this;
        }
        
        public Update SetUpdateType(params SetUpdateType[] setUpdateTypes)
        {
            this.setUpdateTypes = setUpdateTypes;

            return this;
        }
        
        public Update ListUpdateType(params ListUpdateType[] listUpdateTypes)
        {
            this.listUpdateTypes = listUpdateTypes;

            return this;
        }





        //Returns e.g. "name text, " or "name text static, "
        private void AppendVariableRow(StringBuilder sb, Column variable)
        {
            if(variable.GetColumnType().StartsWith("MAP<"))
            {
                if (mapUpdateTypes[mapUpdateTypesCounter] == CassandraQueryBuilder.MapUpdateType.ADD)
                    sb.Append(variable.GetName() + " = " + variable.GetName() + " + ?");
                else //SetUpdateType.Remove
                    sb.Append(variable.GetName() + " = " + variable.GetName() + " - ?");
                
                mapUpdateTypesCounter++;
            }
            else if(variable.GetColumnType().StartsWith("SET<"))
            {
                if (setUpdateTypes[setUpdateTypesCounter] == CassandraQueryBuilder.SetUpdateType.ADD)
                    sb.Append(variable.GetName() + " = " + variable.GetName() + " + ?");
                else //SetUpdateType.Remove
                    sb.Append(variable.GetName() + " = " + variable.GetName() + " - ?");

                setUpdateTypesCounter++;
            }
            else if(variable.GetColumnType().StartsWith("LIST<"))
            {
                if (listUpdateTypes[listUpdateTypesCounter] == CassandraQueryBuilder.ListUpdateType.PREPEND)
                    sb.Append(variable.GetName() + " = ? + " + variable.GetName());
                else if (listUpdateTypes[listUpdateTypesCounter] == CassandraQueryBuilder.ListUpdateType.APPEND)
                    sb.Append(variable.GetName() + " = " + variable.GetName() + " + ?");
                else if (listUpdateTypes[listUpdateTypesCounter] == CassandraQueryBuilder.ListUpdateType.REPLACE_ALL)
                    sb.Append(variable.GetName() + " = ?");
                else //ListUpdateType.SPECIFY_INDEX_TO_OVERWRITE
                    sb.Append(variable.GetName()+ "[?] = ?");

                listUpdateTypesCounter++;
            }
            else
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

        //Om man har ttl så ska den ligga sist i valuesVariables
        //UPDATE ks.tb SET v1 = ?, v2 = ? WHERE v1 = ? AND v2 = ? IF EXISTS;
        public override String ToString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (table == null)
                throw new NullReferenceException("TableName cannot be null");
            if (updateColumns == null)
                throw new NullReferenceException("Variables cannot be null");
            if (whereColumns == null)
                throw new NullReferenceException("WhereVariables cannot be null");



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

            AppendVariableRows(sb, updateColumns, ",");


            sb.Append(" WHERE ");


            AppendVariableRows(sb, whereColumns, " AND");
            

            if (ifExists)
                sb.Append(" IF EXISTS");


            sb.Append(";");

            

            return sb.ToString();
        }

    }
}
