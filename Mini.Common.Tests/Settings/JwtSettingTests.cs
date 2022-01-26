using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Settings;
using System.IO;

namespace Mini.Common.Tests.Settings;

[TestClass()]
public class JwtSettingTests
{
    private JwtSetting jwtSetting = new();

    [TestInitialize]
    public void BeforeEachTest()
    {
        jwtSetting = new()
        {
            Audience = "someAudience",
            Issuer = "someIssuer",
            ExpirationMinutes = 30
        };
    }

    //[ExpectedException(typeof(InvalidDataException))]
    [ExcludeFromCodeCoverage]
    [TestMethod()]
    public void EnsureIsValidIssuerEmptyTest()
    {
        jwtSetting = new();

        var ex = Assert.ThrowsException<InvalidDataException>(() => jwtSetting.EnsureIsValid());

        Assert.IsNotNull(ex);
        Assert.AreEqual("Issuer is null or whitespace.", ex.Message);
    }

    [ExcludeFromCodeCoverage]
    [TestMethod()]
    public void EnsureIsValidAudienceEmptyTest()
    {
        jwtSetting = new()
        {
            Issuer = "someIssuer"
        };

        var ex = Assert.ThrowsException<InvalidDataException>(() => jwtSetting.EnsureIsValid());

        Assert.IsNotNull(ex);
        Assert.AreEqual("Audience is null or whitespace.", ex.Message);
    }

    [TestMethod()]
    public void ToStringTest()
    {
        jwtSetting.EnsureIsValid();

        string toString = jwtSetting.ToString();

        Assert.IsNotNull(toString);
        Assert.AreEqual("Issuer:someIssuer, Audience:someAudience, ExpirationMinutes:30", toString);
    }
}
