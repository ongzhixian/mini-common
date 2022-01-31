﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Requests;

namespace Mini.Common.Tests.Requests;

[TestClass()]
public class LoginRequestTests
{
    private LoginRequest loginRequest = new();

    [TestInitialize]
    public void BeforeEachTest()
    {
        loginRequest = new LoginRequest()
        {
            Username = "someUsername",
            Password = "somePassword"
        };
        
    }

    [TestMethod()]
    public void ToStringTest()
    {
        loginRequest = new LoginRequest("someUsername", "somePassword");

        string toString = loginRequest.ToString();

        Assert.IsNotNull(toString);
        Assert.AreEqual("Username:someUsername, Password:somePassword", toString);

    }
}
