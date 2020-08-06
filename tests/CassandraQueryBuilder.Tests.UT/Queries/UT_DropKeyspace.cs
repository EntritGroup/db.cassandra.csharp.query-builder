using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


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
                    .Keyspace(Variables.keyspace)
                    .ToString()
            );
        }

        [TestMethod]
        public void UT_DropKeyspace_GetString_DataIsNullOrInvalid()
        {
            Assert.ThrowsException<NullReferenceException>(
                () => {
                    new DropKeyspace()
                        .ToString()
                    ;
                }
            );


        }


    }
}
