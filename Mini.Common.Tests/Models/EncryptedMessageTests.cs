using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Models;

namespace Mini.Common.Tests.Models;

[TestClass()]
public class SecurityCredentialTests
{
    private SecurityCredential securityCredential = new();

    [TestInitialize]
    public void BeforeEachTest()
    {
        securityCredential = new SecurityCredential
        {
            SecurityAlgorithm = "someSecurityAlgorithm",
            SecurityDigest = "someSecurityDigest",
            Xml = "someXml"
        };

    }

    [TestMethod()]
    public void ToStringTest()
    {
        securityCredential = new SecurityCredential(
            "someSecurityAlgorithm",
            "someSecurityDigest",
            "someXml"
        ); 

        string toString = securityCredential.ToString();

        Assert.IsNotNull(toString);
        Assert.AreEqual("SecurityAlgorithm:someSecurityAlgorithm, SecurityDigest:someSecurityDigest, Xml:someXml", toString);

    }
}
