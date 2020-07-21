using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB.CassandraQueryBuilder
{
    //http://stackoverflow.com/questions/630803/associating-enums-with-strings-in-c-sharp

    //CQL types https://docs.datastax.com/en/cql/3.0/cql/cql_reference/cql_data_types_c.html
    public class CompactionStrategy
    {
        public string Value { get; private set; }

        private CompactionStrategy(string value) { Value = value; }

        public static CompactionStrategy SizeTieredCompactionStrategy { get { return new CompactionStrategy("SizeTieredCompactionStrategy"); } }

        /// <summary>
        /// 1. Perfect Fit: Time Series Fact Data, Deletes by Default TTL: When you ingest fact data that is ordered in time, with no deletes or overwrites. This is the standard “time series” use case.
        /// 2. OK Fit: Time-Ordered, with limited updates across whole data set, or only updates to recent data: When you ingest data that is (mostly) ordered in time, but revise or delete a very small proportion of the overall data across the whole timeline.
        /// 3. Not a Good Fit: many partial row updates or deletions over time: When you need to partially revise or delete fields for rows that you read together. Also, when you revise or delete rows within clustered reads.
        /// https://www.datastax.com/dev/blog/dtcs-notes-from-the-field
        /// </summary>
        public static CompactionStrategy DateTieredCompactionStrategy { get { return new CompactionStrategy("DateTieredCompactionStrategy"); } }
        public static CompactionStrategy LeveledCompactionStrategy { get { return new CompactionStrategy("LeveledCompactionStrategy"); } }


    }
}
