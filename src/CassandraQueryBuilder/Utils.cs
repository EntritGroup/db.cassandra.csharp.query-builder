using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
