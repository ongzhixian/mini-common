using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Mini.Common.Helpers.Tests
{
    [TestClass()]
    public class EnvironmentHelperTests
    {
        [TestMethod()]
        public void GetVariableTest()
        {
            Environment.SetEnvironmentVariable("someVariable", "someValue");

            string someValue = EnvironmentHelper.GetVariable("someVariable");

            Assert.AreEqual<string>("someValue", someValue);
        }

        [ExcludeFromCodeCoverage]
        [TestMethod()]
        public void GetVariableNullTest()
        {
            Environment.SetEnvironmentVariable("someVariable", null);

            var ex = Assert.ThrowsException<InvalidOperationException>(() => 
                EnvironmentHelper.GetVariable("someVariable"));

            Assert.IsNotNull(ex);
            Assert.AreEqual<string>("Environment variable someVariable does not exists.", ex.Message);
        }
    }
}