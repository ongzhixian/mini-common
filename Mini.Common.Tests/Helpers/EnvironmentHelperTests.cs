using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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