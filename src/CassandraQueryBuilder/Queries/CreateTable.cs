using System;
using System.Text;
using System.Collections.Generic;

namespace CassandraQueryBuilder
{
    public class CreateTable : Query
    {
        private String keyspace;
        private String table;
        private Column[] partitionKeys;
        private Column[] clusteringKeys;
        private Column[] colums;

        private bool isClusteringKeysOrderByAscSet = false;

        private bool isDbCompactionStrategySet = false;

        private bool isGcGraceSecondsSet = false;

        private List<String> withProperties = new List<string>();

        public CreateTable()
        {

        }

        public CreateTable Keyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        public CreateTable Table(String table)
        {
            this.table = table;

            return this;
        }

        public CreateTable PartitionKeys(params Column[] partitionKeys)
        {
            this.partitionKeys = partitionKeys;

            return this;
        }

        public CreateTable ClusteringKeys(params Column[] clusteringKeys)
        {
            this.clusteringKeys = clusteringKeys;

            return this;
        }

        public CreateTable ClusteringKeysOrderByASC(params Boolean[] clusteringKeysOrderByASC)
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

        public CreateTable Columns(params Column[] columns)
        {
            this.colums = columns;

            return this;
        }

        public CreateTable CompactionStrategy(CompactionStrategy dbCompactionStrategy)
        {
            if (isDbCompactionStrategySet)
                throw new Exception("SetCompactionStrategy already set");

            isDbCompactionStrategySet = true;
            
            withProperties.Add("compaction = { 'class' : '" + dbCompactionStrategy.Value + "' }");

            return this;
        }

        public CreateTable GcGrace(int gcGraceSeconds)
        {
            if (isGcGraceSecondsSet)
                throw new Exception("SetGcGrace already set");

            isGcGraceSecondsSet = true;

            withProperties.Add("gc_grace_seconds = " + gcGraceSeconds);

            return this;
        }





        //Returns e.g. "name text, " or "name text static, "
        private void AppendColumnRow(StringBuilder sb, Column column)
        {
            String static_ = "";
            if (column.IsStatic())
                static_ = " STATIC";

            sb.Append(column.Name() + " " + column.ColumnType() + static_ + ", ");
        }

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendColumnRows(StringBuilder sb, Column[] column)
        {
            if (column == null)
                return;
            
            for (int i = 0; i < column.Length; i++)
                AppendColumnRow(sb, column[i]);
        }

        //Returns e.g. "name" or "name, address"
        private void AppendColumnNames(StringBuilder sb, Column[] column)
        {
            if (column != null)
            {
                for (int i = 0; i < column.Length; i++)
                {
                    if (i > 0)
                        sb.Append(", ");
                    sb.Append(column[i].Name());
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
                    sb.Append(clusteringKeys[i].Name() + " " + (clusteringKeysOrderByASC[i] ? Variables.ASCENDING : Variables.DESCENDING));
                }
                sb.Append(")");
            }
        }

        public override String ToString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (table == null)
                throw new NullReferenceException("TableName cannot be null");
            if (partitionKeys == null)
                throw new NullReferenceException("PartitionKeys cannot be null");



            StringBuilder sb = new StringBuilder();

            sb.Append("CREATE TABLE " + keyspace + "." + table + " (");


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
