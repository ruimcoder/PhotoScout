using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoOrganizaer.FileManagement;
using System.Collections.Generic;

namespace PhotoOrganizaer.FileManagement.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void GetFilePropertiesTest()
        {

            string sampleFile = "";
            Dictionary<String, String> result = PhotoOrganizaer.FileManagement.Properties.GetFileProperties(sampleFile);

            if (result.Count > 0)
            {
                foreach (KeyValuePair<String, String> val in result)
                {
                    Console.WriteLine("{0} :: {1}", val.Key, val.Value);
                }

                //Assert.s

            }


            Assert.Fail();
        }
    }
}
