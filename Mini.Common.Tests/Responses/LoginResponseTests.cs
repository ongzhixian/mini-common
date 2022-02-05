using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Models;
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
            ExpiryDateTime = DateTime.MaxValue,
            Signing = new SecurityCredential
            {
                SecurityAlgorithm = SecurityAlgorithms.RsaSsaPssSha256,
                SecurityDigest = SecurityAlgorithms.RsaSsaPssSha256Signature,
                Xml = "someXml"
            }
        };
    }

    [TestMethod()]
    public void ToStringTest()
    {
        loginResponse = new LoginResponse("someJwt", DateTime.MaxValue,
            new SecurityCredential
            {
                SecurityAlgorithm = SecurityAlgorithms.RsaSsaPssSha256,
                SecurityDigest = SecurityAlgorithms.RsaSsaPssSha256Signature,
                Xml = "someXml"
            });

        var toString = loginResponse.ToString();

        Assert.IsNotNull(toString);
        Assert.AreEqual("Jwt:someJwt, ExpiryDateTime:9999-12-31T23:59:59, Signing:SecurityAlgorithm:PS256, SecurityDigest:http://www.w3.org/2007/05/xmldsig-more#sha256-rsa-MGF1, Xml:someXml", toString);
    }
}
