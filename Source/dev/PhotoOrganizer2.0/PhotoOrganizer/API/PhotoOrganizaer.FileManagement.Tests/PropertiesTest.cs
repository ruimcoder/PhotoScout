using System.Collections.Generic;
// <copyright file="PropertiesTest.cs">Copyright ©  2014</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoOrganizaer.FileManagement;

namespace PhotoOrganizaer.FileManagement
{
    [TestClass]
    [PexClass(typeof(Properties))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PropertiesTest
    {
        [PexMethod(MaxRunsWithoutNewTests = 200)]
        public Dictionary<string, string> GetFileProperties(string filePath)
        {
            Dictionary<string, string> result = Properties.GetFileProperties(filePath);
            return result;
            // TODO: add assertions to method PropertiesTest.GetFileProperties(String)
        }
    }
}
