using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Models;

namespace Mini.Common.Tests.Models;

[TestClass()]
public class EncryptedMessageTests
{
    private EncryptedMessage encryptedMessage = new(
        Encoding.UTF8.GetBytes("someIV"),
        Encoding.UTF8.GetBytes("someEncryptedSessionKey"),
        Encoding.UTF8.GetBytes("someEncryptedMessageBytes")
        );

    [TestInitialize]
    public void BeforeEachTest()
    {
        encryptedMessage = new EncryptedMessage
        {
            IV = Encoding.UTF8.GetBytes("someIV"),
            EncryptedSessionKey = Encoding.UTF8.GetBytes("someEncryptedSessionKey"),
            EncryptedMessageBytes = Encoding.UTF8.GetBytes("someEncryptedMessageBytes"),
        };
    }

    [TestMethod()]
    public void ToStringTest()
    {
        string toString = encryptedMessage.ToString();

        Assert.IsNotNull(toString);
        Assert.AreEqual("IV:6, EncryptedSessionKey:23, EncryptedMessageBytes:25", toString);
    }
}
