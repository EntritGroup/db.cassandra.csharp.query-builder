using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Cassandra.QueryBuilder
{
    public interface IPreparedStatement : IQuery
    {
        Task<PreparedStatement> GetPreparedStatement();
    }
}
