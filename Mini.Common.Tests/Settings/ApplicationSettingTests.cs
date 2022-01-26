using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Settings;

namespace Mini.Common.Tests.Settings;

[TestClass()]
public class ApplicationSettingTests
{
    private ApplicationSetting applicationSetting = new();

    [TestInitialize]
    public void BeforeEachTest()
    {
        applicationSetting = new()
        {
            Name = "someName",
            Version = "someVersion"
        };
    }

    [TestMethod()]
    public void ToStringTest()
    {
        string toString = applicationSetting.ToString();

        Assert.IsNotNull(toString);
        Assert.AreEqual($"Name:someName, Version:someVersion", toString);
    }
}
