﻿using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//http://www.datastax.com/dev/blog/materialized-view-performance-in-cassandra-3-x
//https://docs.datastax.com/en/cql/3.3/cql/cql_using/useCreateMV.html
//http://www.datastax.com/dev/blog/new-in-cassandra-3-0-materialized-views
namespace DB.Cassandra.QueryBuilder
{
    public class DBCreateMaterializedView : IQuery
    {
        String keyspace;
        String toTableName; //ToMaterializedView(Name)
        String fromTableName;
        DBColumn[] partitionKeys;
        DBColumn[] clusteringKeys;
        Boolean[] clusteringKeysOrderByASC;
        DBColumn[] columns;
        private CompactionStrategy dbCompactionStrategy;

        public DBCreateMaterializedView()
        {

        }

        public DBCreateMaterializedView SetKeyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        public DBCreateMaterializedView SetToTableName(String toTableName)
        {
            this.toTableName = toTableName;

            return this;
        }

        public DBCreateMaterializedView SetFromTableName(String fromTableName)
        {
            this.fromTableName = fromTableName;

            return this;
        }

        public DBCreateMaterializedView SetPartitionKeys(params DBColumn[] partitionKeys)
        {
            this.partitionKeys = partitionKeys;

            return this;
        }
        
        public DBCreateMaterializedView SetClusteringKeys(params DBColumn[] clusteringKeys)
        {
            this.clusteringKeys = clusteringKeys;

            return this;
        }

        public DBCreateMaterializedView SetClusteringKeysOrderByASC(params Boolean[] clusteringKeysOrderByASC)
        {
            this.clusteringKeysOrderByASC = clusteringKeysOrderByASC;

            return this;
        }

        public DBCreateMaterializedView SetColumns(params DBColumn[] columns)
        {
            this.columns = columns;

            return this;
        }

        public DBCreateMaterializedView SetCompactionStrategy(CompactionStrategy dbCompactionStrategy)
        {
            this.dbCompactionStrategy = dbCompactionStrategy;

            return this;
        }








        //Returns e.g. "name text, " or "name text static, "
        private void AppendColumnRow(StringBuilder sb, DBColumn column)
        {
            sb.Append(column.GetName());
        }

        //Returns e.g. "name text, address text, " or "" if null
        private void AppendColumnRows(StringBuilder sb, DBColumn[] column)
        {
            if (column == null)
                return;

            for (int i = 0; i < column.Length; i++)
            {
                AppendColumnRow(sb, column[i]);

                if (i < column.Length - 1)
                    sb.Append(", ");
            }
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

        //Returns e.g. "name IS NOT NULL AND address IS NOT NULL"
        private void AppendColumnNamesWithIsNotNull(StringBuilder sb, DBColumn[] columns)
        {
            if (columns != null)
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    if (i > 0)
                        sb.Append("AND ");
                    sb.Append(columns[i].GetName() + " IS NOT NULL ");
                }
            }
        }

        //Returns e.g. "name" or "name, address"
        private void AppendClusteringOrder(StringBuilder sb)
        {
            if (clusteringKeys != null && clusteringKeysOrderByASC != null)
            {
                sb.Append(" WITH CLUSTERING ORDER BY (");


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
            if (toTableName == null)
                throw new NullReferenceException("MaterializedViewName cannot be null");
            if (fromTableName == null)
                throw new NullReferenceException("FromTableName cannot be null");
            if (partitionKeys == null)
                throw new NullReferenceException("PartitionKeys cannot be null");
            if (clusteringKeys == null && clusteringKeysOrderByASC != null)
                throw new NullReferenceException("ClusteringKeys cannon be null when clusteringKeysOrderByASC is not null");
            if (clusteringKeys != null && clusteringKeysOrderByASC != null && clusteringKeys.Length < clusteringKeysOrderByASC.Length)
                throw new Exception("ClusteringKeys lenth must be larger or equal to clusteringkeysOrderByASC length");



            StringBuilder sb = new StringBuilder();

            sb.Append("CREATE MATERIALIZED VIEW " + keyspace + "." + toTableName + " AS SELECT ");


            AppendColumnRows(sb, partitionKeys);
            if (clusteringKeys != null)
                sb.Append(", ");
            AppendColumnRows(sb, clusteringKeys);
            if (columns != null)
                sb.Append(", ");
            AppendColumnRows(sb, columns);

            sb.Append(" FROM " + keyspace + "." + fromTableName + " WHERE ");



            //----Create all WHERE name IS NOT NULL AND address IS NOT NULL etc"

            AppendColumnNamesWithIsNotNull(sb, partitionKeys);
            if (clusteringKeys != null)
                sb.Append("AND ");
            AppendColumnNamesWithIsNotNull(sb, clusteringKeys);



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


            //----

            //sb.Append(")");


            AppendClusteringOrder(sb);


            //---- Set compaction strategy

            if (dbCompactionStrategy != null)
            {
                if (clusteringKeysOrderByASC == null)
                    sb.Append(" WITH ");
                else
                    sb.Append(" AND ");

                sb.Append("compaction = { 'class' : '" + dbCompactionStrategy.Value + "' }");
            }


            //----


            sb.Append(";");




            return sb.ToString();
        }
        
    }
}