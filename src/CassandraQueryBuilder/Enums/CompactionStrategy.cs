namespace CassandraQueryBuilder
{
    public class CompactionStrategy
    {
        public string Value { get; private set; }

        private CompactionStrategy(string value) { Value = value; }

        public static CompactionStrategy SizeTieredCompactionStrategy { get { return new CompactionStrategy("SizeTieredCompactionStrategy"); } }

        public static CompactionStrategy TimeWindowCompactionStrategy { get { return new CompactionStrategy("TimeWindowCompactionStrategy"); } }

        public static CompactionStrategy LeveledCompactionStrategy { get { return new CompactionStrategy("LeveledCompactionStrategy"); } }


    }
}
