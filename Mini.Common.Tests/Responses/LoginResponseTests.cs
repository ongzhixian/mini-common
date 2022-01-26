using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Responses;
using System;

namespace Mini.Common.Tests.Responses;

[TestClass()]
public class LoginResponseTests
{
    private LoginResponse loginResponse = new();

    [TestInitialize]
    public void BeforeEachTest()
    {
        loginResponse = new LoginResponse()
        {
            Jwt = "someJwt",
            ExpiryDateTime = DateTime.MaxValue
        };
    }

    [TestMethod()]
    public void ToStringTest()
    {
        var toString = loginResponse.ToString();

        Assert.IsNotNull(toString);
        Assert.AreEqual("Jwt:someJwt, ExpiryDateTime:9999-12-31T23:59:59", toString);
    }
}
