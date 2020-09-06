namespace CassandraQueryBuilder
{
    //= | < | > | <= | >= | CONTAINS | CONTAINS KEY
    public class WhereOperator
    {
        public string Value { get; private set; }

        private WhereOperator(string value) { Value = value; }

        public static WhereOperator EqualTo { get { return new WhereOperator("="); } }

        public static WhereOperator SmallerThan { get { return new WhereOperator("<"); } }
        
        public static WhereOperator LargerThan { get { return new WhereOperator(">"); } }
        
        public static WhereOperator SmallerOrEqualThan { get { return new WhereOperator("<="); } }
        
        public static WhereOperator LargerOrEqualThan { get { return new WhereOperator(">="); } }
        
        public static WhereOperator Contains { get { return new WhereOperator("CONTAINS"); } }
        
        public static WhereOperator ContainsKey { get { return new WhereOperator("CONTAINS KEY"); } }

    }
}
