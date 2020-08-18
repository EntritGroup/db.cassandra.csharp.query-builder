using System;
using System.Text;

namespace CassandraQueryBuilder
{
    internal class Utils
    {
        internal static bool CompareStrings(String s1, String s2)
        {
            if (s1 == null && s2 == null)
                return true;
            else if (s1 == null || s2 == null)
                return false;

            return s1.CompareTo(s2) == 0;
        }

        

        /// <summary>
        /// Add column name to the string builder
        /// </summary>
        /// <param name="sb">String Builder</param>
        /// <param name="column">Column</param>
        internal static void AppendColumnRow(StringBuilder sb, Column column)
        {
            sb.Append(column.Name());
        }

        /// <summary>
        /// Add column name and a suffix to the string (name + suffix)
        /// </summary>
        /// <param name="sb">String builder</param>
        /// <param name="column">Column</param>
        /// <param name="suffix">Suffix</param>
        internal static void AppendColumnRow(StringBuilder sb, Column column, String suffix)
        {
            sb.Append(column.Name() + suffix);
        }

        /// <summary>
        /// Add prefix, column name and suffix to the string (prefix + name + suffix)
        /// </summary>
        /// <param name="sb">String builder</param>
        /// <param name="column">Column</param>
        /// <param name="prefix">Prefix</param>
        /// <param name="suffix">Suffix</param>
        internal static void AppendColumnRow(StringBuilder sb, Column column, String prefix, String suffix)
        {
            sb.Append(prefix + column.Name() + suffix);
        }

        
        /// <summary>
        /// Add column names after each other
        /// 
        /// E.g.!-- name1, name2, ...
        /// </summary>
        /// <param name="sb">Stringbuilder</param>
        /// <param name="columns">Columns</param>
        internal static void AppendColumnRows(StringBuilder sb, Column[] columns)
        {
            if (columns == null)
                return;

            for (int i = 0; i < columns.Length; i++)
            {
                AppendColumnRow(sb, columns[i]);

                if (i < columns.Length - 1)
                    sb.Append(", ");
            }
        }
    }
}
