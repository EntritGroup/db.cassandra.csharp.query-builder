using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CassandraQueryBuilder
{
    public interface IQuery
    {
        String GetString();
    }
}
