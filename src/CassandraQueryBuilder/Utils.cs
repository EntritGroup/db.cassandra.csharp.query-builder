using System;

namespace CassandraQueryBuilder
{
    public class Utils
    {
        public static bool CompareStrings(String s1, String s2)
        {
            if (s1 == null && s2 == null)
                return true;
            else if (s1 == null || s2 == null)
                return false;

            return s1.CompareTo(s2) == 0;
        }
    }
}
