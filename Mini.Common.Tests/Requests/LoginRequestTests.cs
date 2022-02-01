using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Models;
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
            Password = "somePassword",
            Encrypting = new SecurityCredential
            {
                SecurityAlgorithm = SecurityAlgorithms.RsaOAEP,
                SecurityDigest = SecurityAlgorithms.Aes256CbcHmacSha512,
                Xml = "someXml"
            }
        };
        
    }

    [TestMethod()]
    public void ToStringTest()
    {
        loginRequest = new LoginRequest("someUsername", "somePassword", new SecurityCredential
        {
            SecurityAlgorithm = SecurityAlgorithms.RsaOAEP,
            SecurityDigest = SecurityAlgorithms.Aes256CbcHmacSha512,
            Xml = "someXml"
        });

        string toString = loginRequest.ToString();

        Assert.IsNotNull(toString);
        Assert.AreEqual("Username:someUsername, Password:************, Encrypting:SecurityAlgorithm:RSA-OAEP, SecurityDigest:A256CBC-HS512, Xml:someXml", toString);

    }
}
