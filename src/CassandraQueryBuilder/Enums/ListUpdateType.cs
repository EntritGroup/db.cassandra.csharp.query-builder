using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraQueryBuilder
{
    //For the LIST<...> Collection
    public enum ListUpdateType
    {
        PREPEND,
        APPEND,
        REPLACE_ALL,
        SPECIFY_INDEX_TO_OVERWRITE
    }
}
