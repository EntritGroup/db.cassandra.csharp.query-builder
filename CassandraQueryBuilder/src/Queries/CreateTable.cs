using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Cassandra.QueryBuilder
{
    public class DBCreateTable : IQuery
    {
        private String keyspace;
        private String tableName;
        private DBColumn[] partitionKeys;
        private DBColumn[] clusteringKeys;
        private DBColumn[] colums;

        private bool isClusteringKeysOrderByAscSet = false;

        private bool isDbCompactionStrategySet = false;

        private bool isGcGraceSecondsSet = false;

        private List<String> withProperties = new List<string>();

        public DBCreateTable()
        {

        }

        public DBCreateTable SetKeyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        public DBCreateTable SetTableName(String tableName)
        {
            this.tableName = tableName;

            return this;
        }

        public DBCreateTable SetPartitionKeys(params DBColumn[] partitionKeys)
        {
            this.partitionKeys = partitionKeys;

            return this;
        }

        public DBCreateTable SetClusteringKeys(params DBColumn[] clusteringKeys)
        {
            this.clusteringKeys = clusteringKeys;

            return this;
        }

        public DBCreateTable SetClusteringKeysOrderByASC(params Boolean[] clusteringKeysOrderByASC)
        {
            if (isClusteringKeysOrderByAscSet)
                throw new Exception("SetClusteringKeysOrderByASC already set");
            if (clusteringKeys == null)
                throw new NullReferenceException("ClusteringKeys cannon be null when clusteringKeysOrderByASC is not null");
            if (clusteringKeys != null && clusteringKeys.Length < clusteringKeysOrderByASC.Length)
                throw new Exception("ClusteringKeys lenth must be larger or equal to clusteringkeysOrderByASC length");

            isClusteringKeysOrderByAscSet = true;

            StringBuilder sb = new StringBuilder();

            AppendClusteringOrder(sb, clusteringKeysOrderByASC);

            withProperties.Add(sb.ToString());
            
            return this;
        }

        public DBCreateTable SetColumns(params DBColumn[] columns)
        {
            this.colums = columns;

            return this;
        }

        public DBCreateTable SetCompactionStrategy(CompactionStrategy dbCompactionStrategy)
        {
            if (isDbCompactionStrategySet)
                throw new Exception("SetCompactionStrategy already set");

            isDbCompactionStrategySet = true;
            
            withProperties.Add("compaction = { 'class' : '" + dbCompactionStrategy.Value + "' }");

            return this;
        }

        public DBCreateTable SetGcGrace(int gcGraceSeconds)
        {
            if (isGcGraceSecondsSet)
                throw new Exception("SetGcGrace already set");

            isGcGraceSecondsSet = true;

            withProperties.Add("gc_grace_seconds = " + gcGraceSeconds);

            return this;
        }





        //Returns e.g. "name text, " or "name text static, "
        private void AppendColumnRow(StringBuilder sb, DBColumn column)
        {
            String static_ = "";
            if (column.IsStatic())
                static_ = " STATIC";

            sb.Append(column.GetName() + " " + column.GetDBColumnType() + static_ + ", ");
        }

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendColumnRows(StringBuilder sb, DBColumn[] column)
        {
            if (column == null)
                return;
            
            for (int i = 0; i < column.Length; i++)
                AppendColumnRow(sb, column[i]);
        }

        //Returns e.g. "name" or "name, address"
        private void AppendColumnNames(StringBuilder sb, DBColumn[] column)
        {
            if (column != null)
            {
                for (int i = 0; i < column.Length; i++)
                {
                    if (i > 0)
                        sb.Append(", ");
                    sb.Append(column[i].GetName());
                }
            }
        }

        //Returns e.g. "name" or "name, address"
        private void AppendClusteringOrder(StringBuilder sb, Boolean[] clusteringKeysOrderByASC)
        {
            if (clusteringKeys != null && clusteringKeysOrderByASC != null)
            {
                sb.Append("CLUSTERING ORDER BY (");


                int length = clusteringKeysOrderByASC.Length;

                for (int i = 0; i < length; i++)
                {
                    if (i > 0)
                        sb.Append(", ");
                    sb.Append(clusteringKeys[i].GetName() + " " + (clusteringKeysOrderByASC[i] ? Variables.ASCENDING : Variables.DESCENDING));
                }
                sb.Append(")");
            }
        }

        public String GetString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (tableName == null)
                throw new NullReferenceException("TableName cannot be null");
            if (partitionKeys == null)
                throw new NullReferenceException("PartitionKeys cannot be null");



            StringBuilder sb = new StringBuilder();

            sb.Append("CREATE TABLE " + keyspace + "." + tableName + " (");


            AppendColumnRows(sb, partitionKeys);
            AppendColumnRows(sb, clusteringKeys);
            AppendColumnRows(sb, colums);




            //----Create primary and clustering key "PRIMARY KEY ((pk) ck)" or "PRIMARY KEY (pk)"

            sb.Append("PRIMARY KEY (");

            if (clusteringKeys != null)
                sb.Append("(");

            AppendColumnNames(sb, partitionKeys);

            sb.Append(")");

            if (clusteringKeys != null)
                sb.Append(", ");

            AppendColumnNames(sb, clusteringKeys);

            if (clusteringKeys != null)
                sb.Append(")");


            //---- Set compaction strategy


            sb.Append(")");

            if(withProperties != null && withProperties.Count > 0)
            {
                sb.Append(" WITH ");
                bool first = true;

                foreach (String s in withProperties)
                {
                    if (first)
                        first = false;
                    else
                        sb.Append(" AND ");

                    sb.Append(s);
                }
            }



            
            //----

            sb.Append(";");




            return sb.ToString();
        }
        
    }
}
