using System;
using System.Text;

//http://www.datastax.com/dev/blog/materialized-view-performance-in-cassandra-3-x
//https://docs.datastax.com/en/cql/3.3/cql/cql_using/useCreateMV.html
//http://www.datastax.com/dev/blog/new-in-cassandra-3-0-materialized-views
namespace CassandraQueryBuilder
{
    public class CreateMaterializedView : Query
    {
        String keyspace;
        String toTable; //ToMaterializedView(Name)
        String fromTable;
        Column[] partitionKeys;
        Column[] clusteringKeys;
        Boolean[] clusteringKeysOrderByASC;
        Column[] columns;
        private CompactionStrategy dbCompactionStrategy;

        /// <summary>
        /// To create materialized views queries
        /// </summary>
        public CreateMaterializedView()
        {

        }

        /// <summary>
        /// Set keyspace name
        /// </summary>
        /// <param name="keyspace">Keyspace name</param>
        /// <returns>CreateMaterializedView</returns>
        public CreateMaterializedView Keyspace(String keyspace)
        {
            this.keyspace = keyspace;

            return this;
        }

        /// <summary>
        /// Set to table name (the materialized view table name)
        /// </summary>
        /// <param name="toTable">To Table name</param>
        /// <returns>CreateMaterlializedView</returns>
        public CreateMaterializedView ToTable(String toTable)
        {
            this.toTable = toTable;

            return this;
        }

        /// <summary>
        /// Set from table name (the table to build the materialized view from)
        /// </summary>
        /// <param name="fromTable">Table name</param>
        /// <returns>CreateMaterializedView</returns>
        public CreateMaterializedView FromTable(String fromTable)
        {
            this.fromTable = fromTable;

            return this;
        }

        /// <summary>
        /// The columns for the partition keys
        /// </summary>
        /// <param name="partitionKeys">The columns used for partition keys</param>
        /// <returns>CreateMaterializedView</returns>
        public CreateMaterializedView PartitionKeys(params Column[] partitionKeys)
        {
            this.partitionKeys = partitionKeys;

            return this;
        }
        
        /// <summary>
        /// The columns for the clustering keys
        /// </summary>
        /// <param name="partitionKeys">The columns used for clustering keys</param>
        /// <returns>CreateMaterializedView</returns>
        public CreateMaterializedView ClusteringKeys(params Column[] clusteringKeys)
        {
            this.clusteringKeys = clusteringKeys;

            return this;
        }

        /// <summary>
        /// If the clustering keys should be ordered by ASC or DESC
        /// </summary>
        /// <param name="clusteringKeysOrderByASC">ASC = true, DESC = false</param>
        /// <returns>CreateMaterializedView</returns>
        public CreateMaterializedView ClusteringKeysOrderByASC(params Boolean[] clusteringKeysOrderByASC)
        {
            this.clusteringKeysOrderByASC = clusteringKeysOrderByASC;

            return this;
        }

        /// <summary>
        /// The general columns for the table
        /// </summary>
        /// <param name="columns">The columns in the table</param>
        /// <returns>CreateMaterializedView</returns>
        public CreateMaterializedView Columns(params Column[] columns)
        {
            this.columns = columns;

            return this;
        }

        /// <summary>
        /// Set compaction strategy
        /// </summary>
        /// <param name="dbCompactionStrategy">SizeTieredCompactionStrategy, DateTieredCompactionStrategy, "LeveledCompactionStrategy</param>
        /// <returns>CreateMaterializedView</returns>
        public CreateMaterializedView CompactionStrategy(CompactionStrategy dbCompactionStrategy)
        {
            this.dbCompactionStrategy = dbCompactionStrategy;

            return this;
        }





        //Returns e.g. "name IS NOT NULL AND address IS NOT NULL"
        private void AppendColumnNamesWithIsNotNull(StringBuilder sb, Column[] columns)
        {
            if (columns != null)
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    if (i > 0)
                        sb.Append("AND ");
                    sb.Append(columns[i].Name() + " IS NOT NULL ");
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
                    sb.Append(clusteringKeys[i].Name() + " " + (clusteringKeysOrderByASC[i] ? Variables.ASCENDING : Variables.DESCENDING));
                }
                sb.Append(")");
            }
        }

        /// <summary>
        /// Creates the prepared statement string
        /// </summary>
        /// <returns>String</returns>
        public override String ToString()
        {
            if (keyspace == null)
                throw new NullReferenceException("Keyspace cannot be null");
            if (toTable == null)
                throw new NullReferenceException("MaterializedViewName cannot be null");
            if (fromTable == null)
                throw new NullReferenceException("FromTableName cannot be null");
            if (partitionKeys == null)
                throw new NullReferenceException("PartitionKeys cannot be null");
            if (clusteringKeys == null && clusteringKeysOrderByASC != null)
                throw new NullReferenceException("ClusteringKeys cannon be null when clusteringKeysOrderByASC is not null");
            if (clusteringKeys != null && clusteringKeysOrderByASC != null && clusteringKeys.Length < clusteringKeysOrderByASC.Length)
                throw new Exception("ClusteringKeys lenth must be larger or equal to clusteringkeysOrderByASC length");



            StringBuilder sb = new StringBuilder();

            sb.Append("CREATE MATERIALIZED VIEW " + keyspace + "." + toTable + " AS SELECT ");


            Utils.AppendColumnRows(sb, partitionKeys);
            if (clusteringKeys != null)
                sb.Append(", ");
            Utils.AppendColumnRows(sb, clusteringKeys);
            if (columns != null)
                sb.Append(", ");
            Utils.AppendColumnRows(sb, columns);

            sb.Append(" FROM " + keyspace + "." + fromTable + " WHERE ");



            //----Create all WHERE name IS NOT NULL AND address IS NOT NULL etc"

            AppendColumnNamesWithIsNotNull(sb, partitionKeys);
            if (clusteringKeys != null)
                sb.Append("AND ");
            AppendColumnNamesWithIsNotNull(sb, clusteringKeys);



            //----Create primary and clustering key "PRIMARY KEY ((pk) ck)" or "PRIMARY KEY (pk)"

            sb.Append("PRIMARY KEY ((");

            Utils.AppendColumnRows(sb, partitionKeys);

            sb.Append(")");

            if (clusteringKeys != null)
                sb.Append(", ");

            Utils.AppendColumnRows(sb, clusteringKeys);

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
