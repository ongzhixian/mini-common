using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Helpers;

namespace Mini.Common.Tests.Helpers;

[TestClass()]
public class SecurityKeyHelperTests
{
    [TestMethod()]
    public void SymmetricSecurityKeyTest()
    {
        string psv = "A|B|6|8";

        var (sc, ec) = SecurityKeyHelper.SymmetricSecurityKey(psv, HashAlgorithmName.SHA256);

        Assert.IsNotNull(sc);
        Assert.IsNotNull(ec);
    }

    [ExcludeFromCodeCoverage]
    [TestMethod()]
    public void SymmetricSecurityKeyInvalidValueTest()
    {
        string psv = "A|B|6";

        var ex = Assert.ThrowsException<InvalidDataException>(() => SecurityKeyHelper.SymmetricSecurityKey(psv, HashAlgorithmName.SHA256));

        Assert.IsNotNull(ex);
        Assert.AreEqual<string>("Invalid pipe separated value.", ex.Message);
    }

    [ExcludeFromCodeCoverage]
    [TestMethod()]
    public void SymmetricSecurityKeyJustPipesTest()
    {
        string psv = "|||";

        HashAlgorithmName hashAlgorithmName = new HashAlgorithmName();

        var ex = Assert.ThrowsException<InvalidOperationException>(() => SecurityKeyHelper.SymmetricSecurityKey(psv, hashAlgorithmName));

        Assert.IsNotNull(ex);
        Assert.AreEqual<string>("Hash algorithm name is null.", ex.Message);
    }
}
