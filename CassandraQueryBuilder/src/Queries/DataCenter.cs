using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Cassandra.QueryBuilder
{
    public class DBDataCenter
    {
        private String name;
        private int? replicationFactor;


        public DBDataCenter(String name, int replicationFactor)
        {
            this.name = name;
            this.replicationFactor = replicationFactor;
        }


        //public DBDataCenter SetName(String name)
        //{
        //    this.name = name;

        //    return this;
        //}
        
        //public DBDataCenter SetReplicationFactor(int replicationFactor)
        //{
        //    this.replicationFactor = replicationFactor;

        //    return this;
        //}



        public String GetName()
        {
            return name;
        }
        public int GetReplicationFactor()
        {
            if(replicationFactor == null)
                throw new NullReferenceException("ReplicationFactor cannot be null");

            return (int)replicationFactor;
        }


    }
}
