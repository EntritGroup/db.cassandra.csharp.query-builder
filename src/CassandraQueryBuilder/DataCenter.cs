using System;

namespace CassandraQueryBuilder
{
    public class DataCenter
    {
        private String name;
        private int? replicationFactor;


        public DataCenter(String name, int replicationFactor)
        {
            this.name = name;
            this.replicationFactor = replicationFactor;
        }


        //public DataCenter SetName(String name)
        //{
        //    this.name = name;

        //    return this;
        //}
        
        //public DataCenter SetReplicationFactor(int replicationFactor)
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
