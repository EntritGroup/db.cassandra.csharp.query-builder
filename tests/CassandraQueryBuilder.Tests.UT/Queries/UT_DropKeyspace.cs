using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB.CassandraQueryBuilder;


namespace CassandraQueryBuilder.Tests.UT
{
    [TestClass]
    public class UT_DropKeyspace
    {
        [TestMethod]
        public void UT_KDropeyspace_GetString()
        {
            String result = "DROP KEYSPACE IF EXISTS ks;";
            Assert.AreEqual(result,
                new DropKeyspace()
                    .SetName(Variables.keyspace)
                    .GetString()
                )
            ;
        }

        [TestMethod]
        public void UT_DropKeyspace_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new DropKeyspace()
                        .GetString()
                    ;
                }
            );


        }


    }
}
