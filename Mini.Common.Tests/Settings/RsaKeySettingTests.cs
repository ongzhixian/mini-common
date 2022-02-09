using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Settings;
using Moq;
using Moq.Protected;

namespace Mini.Common.Tests.Settings;

[TestClass()]
public class RsaKeySettingTests
{
    private RsaKeySetting rsaKeySetting = new();

    private readonly string privateKeyXml = @"
<RSAKeyValue>
<Modulus>lMlZCTHRb0DkkFqUzhH7YkjCVPSCX7QP5u4r1dbTXmlByWGgBDcqvp3bNLuWJE+MOAUyB84mSRD8H/7AzLhztFzWtssIrU7rbFdgx+DJVzfHyLfbhwLMr+dG8fREo8/Vz9M+G2if6KQTxJWIyEYSvVK0s7ulbRqu2xm1a1c+N/z45Czjq9s3A5c/PXotPnywkbgq6EMypx2HGy+ckobGITcAyC5Ws7EeoFHZOWK5v/yJbeVIlqWYtqDCeIix+Jcb7y1Tz/zqqR/OFbMKV2drrRpOuXPfN3+Tql1YV4Am6V/4MpJ2p8X9qjdkdcMmPRxSeSwojtb3DgclM7mnm3b/YQ==</Modulus>
<Exponent>AQAB</Exponent>
<P>w4dWpPBzT+BL3d/d56yXz1NVibrc4N6eLzKh/3e7Oiy0I4fwHs+C31jsOeQRQAgufM80VprQz4suwRv4GdklajermPyqOhRVO0kGdTZL/6jiRh39Ef2Q0l+f3oGhROo1uWtxbWpnEr7QowgNMTmlkm4ShAKVLZKgnYkCLJIqCls=</P>
<Q>ws1Gsx+usdU58Gi2noCkg3rttE0vDdQrJSrhKkkJLUSr7ePWRORBBpLYxE18/n/1VhAj+Y16dpdje0xnDejknmwbWJ6GXRUnFoSPnJrAFgjzNIgniALAJfQHAeUFwCFaDvpuFHUUd615UjCitg4movnzxzf5XzVGbhwc99uHcfM=</Q>
<DP>WFdLdFYXI12pqWM99TBrnoZ+PS4qIYczXQu5WZ0VAGG2Od9vQ9xputOIV1eN26pWpplPglmMQlkWFLW4UKVlXCou4340wuzw3UpPGqIfkDETmq5t6rTvu7zslDFpVaOkBlRe+Rp722JPDXnTzAvJnPESeIZaNC7tVn/SdaTMTcU=</DP>
<DQ>n6mGP/xAv+rnIR+CRmlj7YvM1lHItknmzwDVKkZQajT1wfZSwYZfsZacalCkmSehmteB1OFbtWWhmQZnFOSEtUAgLcNIl3Rl5DPkTVQdCjJtu+m4lObEPJdFQw1GaFItsUcbAFNx4iFh5baNNjBlBIFfiDJdhuZwgoEyUQgyfHc=</DQ>
<InverseQ>DwKCzXq78EPB7g2hng1WS6JaZu2vThIcoSiqhDHYqAv4zG+K1r62Cd/SRZFLaVcJJK8j+XhUWnvB3fIB8sLEgBLJbHzBhJkAQB39wbWYOOkyyxUb4WLx9FQynQWdKnUo4qz8oGCjjlD1Wd6ZZkahmLJxNP0HhCT3YY4D1uIbMU8=</InverseQ>
<D>JQMdigMBlYpwEVTnNYgVn/J3nUih8grJ9emDsBNby12lxuHpO4M5t+du82RCyk+bkwf/1R88OOrnvVHTRloQNmrt4tUJGIvMVsw5wB80FuVCZrUOADz7DDsdU+0u8g0upB9pkMnrFaVTYZDNUPzVhpE/cIKaeVSs8MYpckVmreh4LCxKT3e4Qe5myhUjBVCcwpWgo3o8Bli3sqVmfrW6dkWij7swIOgzNf7gMcdyy4a2iymYV2NSrZAbW4Pp5P4dMaGLhpTZpKPXs9y5Gjs4XWRW2WLb1glbDSwhFCH/ob07VtGhwP18Q4iAJpJWFBJU2MIxl9jXzxAHK4lWXpmGrQ==</D>
</RSAKeyValue>";

    [TestInitialize]
    public void BeforeEachTest()
    {
        rsaKeySetting = new();
    }

    [TestMethod()]
    public async Task RsaXmlTestAsync()
    {
        var result = await rsaKeySetting.SourceXmlAsync();

    //    Assert.AreEqual(string.Empty, result);
    }

    [TestMethod()]
    public async Task RsaXmlTestHttpAsync()
    {
        Mock<HttpMessageHandler> mockHttpMessageHandler = new();

        mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        )
        .ReturnsAsync(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("<RSAKeyValue><Modulus>2R+FsrRjNC7xfOrRJyM2c1WQSUGFpxH2pWTJPKuAfnh/kZPkcwtvgHhf0RXrG33p2fDYP+qQJSsoLd8FkGo+ahKxvJL8ShrumcrJcVo2Tx3BNMT7kXZryDGqeuRMdCG3vXkiyp7E5A3CnozP1D1SM+vNuxxpGKqod9n8mVpPa7o6T2lM/qhjEyTmYJ+hsh35yQ9pxgQBGCwA4CJm6IxUuMB8DpAFe4z+CTeL+K8pk+isB+X/WdEhCqjOHvJ7NrUdOQ0HqMtFWt9s5oAss4ws5BTWuMbWdcPP60Wi7cw1SoMc7VCRZ8lAXKNblFEHzKhXRiYBlut/OeC9N7pl5VrdCQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>")
        });

        rsaKeySetting = new(new HttpClient(mockHttpMessageHandler.Object));

        var result = await rsaKeySetting.SourceXmlAsync();

        Assert.AreEqual(string.Empty, result);
    }


    [TestMethod()]
    public async Task GetRsaSecurityKeyEnvVarPublicKeyTestAsync()
    {
        Environment.SetEnvironmentVariable("SOME_PRIVATE_KEY", privateKeyXml);

        rsaKeySetting = new()
        {
            SourceType = RsaKeySetting.RsaKeyDataSource.EnvironmentVariable
            , Source = "SOME_PRIVATE_KEY"
        };

        RsaSecurityKey rsaSecurityKey = await rsaKeySetting.GetRsaSecurityKeyAsync(false);

        Assert.AreEqual(rsaSecurityKey.PrivateKeyStatus, PrivateKeyStatus.DoesNotExist);
    }

    [TestMethod()]
    public async Task GetRsaSecurityKeyEnvVarPrivateKeyTestAsync()
    {
        Environment.SetEnvironmentVariable("SOME_PRIVATE_KEY", privateKeyXml);

        rsaKeySetting = new()
        {
            SourceType = RsaKeySetting.RsaKeyDataSource.EnvironmentVariable
            ,
            Source = "SOME_PRIVATE_KEY"
        };

        RsaSecurityKey rsaSecurityKey = await rsaKeySetting.GetRsaSecurityKeyAsync(true);

        Assert.AreEqual(rsaSecurityKey.PrivateKeyStatus, PrivateKeyStatus.Exists);
    }

    [ExcludeFromCodeCoverage]
    [TestMethod()]
    public async Task GetRsaSecurityKeyEnvVarNotExistsTestAsync()
    {
        rsaKeySetting = new()
        {
            SourceType = RsaKeySetting.RsaKeyDataSource.EnvironmentVariable
            , Source = "MISSING_PRIVATE_KEY"
        };

        Assert.AreEqual(String.Empty, await rsaKeySetting.SourceXmlAsync());
    }

    [TestMethod()]
    public async Task GetRsaSecurityKeyFileTestAsync()
    {
        rsaKeySetting = new()
        {
            SourceType = RsaKeySetting.RsaKeyDataSource.File
            , Source = "_Data/UnitTest-PrivateKey.xml"
        };

        RsaSecurityKey rsaSecurityKey = await rsaKeySetting.GetRsaSecurityKeyAsync(false);

        Assert.AreEqual(rsaSecurityKey.PrivateKeyStatus, PrivateKeyStatus.DoesNotExist);
    }

    [TestMethod()]
    public async Task GetRsaSecurityKeyHttpTestAsync()
    {
        Mock<HttpMessageHandler> mockHttpMessageHandler = new();
        mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
        )
        .ReturnsAsync(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("<RSAKeyValue><Modulus>2R+FsrRjNC7xfOrRJyM2c1WQSUGFpxH2pWTJPKuAfnh/kZPkcwtvgHhf0RXrG33p2fDYP+qQJSsoLd8FkGo+ahKxvJL8ShrumcrJcVo2Tx3BNMT7kXZryDGqeuRMdCG3vXkiyp7E5A3CnozP1D1SM+vNuxxpGKqod9n8mVpPa7o6T2lM/qhjEyTmYJ+hsh35yQ9pxgQBGCwA4CJm6IxUuMB8DpAFe4z+CTeL+K8pk+isB+X/WdEhCqjOHvJ7NrUdOQ0HqMtFWt9s5oAss4ws5BTWuMbWdcPP60Wi7cw1SoMc7VCRZ8lAXKNblFEHzKhXRiYBlut/OeC9N7pl5VrdCQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>")
        });

        rsaKeySetting = new(new HttpClient(mockHttpMessageHandler.Object))
        {
            SourceType = RsaKeySetting.RsaKeyDataSource.Http
            , Source = "https://localhost:5001/api/PublicKey/sign"
        };

        RsaSecurityKey rsaSecurityKey = await rsaKeySetting.GetRsaSecurityKeyAsync(false);

        Assert.AreEqual(rsaSecurityKey.PrivateKeyStatus, PrivateKeyStatus.DoesNotExist);
    }

    [ExcludeFromCodeCoverage]
    [TestMethod()]
    public void GetRsaSecurityKeyNullHttpClientTest()
    {
        var ex = Assert.ThrowsException<ArgumentNullException>(() => new RsaKeySetting(null));

        Assert.IsNotNull(ex);
        Assert.AreEqual("Value cannot be null. (Parameter 'httpClient')", ex.Message);
    }

    [TestMethod()]
    public async Task GetRsaSecurityKeyXmlAsyncTestAsync()
    {
        Environment.SetEnvironmentVariable("SOME_PRIVATE_KEY", privateKeyXml);

        rsaKeySetting = new()
        {
            SourceType = RsaKeySetting.RsaKeyDataSource.EnvironmentVariable
            , Source = "SOME_PRIVATE_KEY"
        };

        rsaKeySetting.EnsureIsValid();

        var xml = await rsaKeySetting.GetRsaSecurityKeyXmlAsync(false);

        Assert.AreEqual("<RSAKeyValue><Modulus>lMlZCTHRb0DkkFqUzhH7YkjCVPSCX7QP5u4r1dbTXmlByWGgBDcqvp3bNLuWJE+MOAUyB84mSRD8H/7AzLhztFzWtssIrU7rbFdgx+DJVzfHyLfbhwLMr+dG8fREo8/Vz9M+G2if6KQTxJWIyEYSvVK0s7ulbRqu2xm1a1c+N/z45Czjq9s3A5c/PXotPnywkbgq6EMypx2HGy+ckobGITcAyC5Ws7EeoFHZOWK5v/yJbeVIlqWYtqDCeIix+Jcb7y1Tz/zqqR/OFbMKV2drrRpOuXPfN3+Tql1YV4Am6V/4MpJ2p8X9qjdkdcMmPRxSeSwojtb3DgclM7mnm3b/YQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>", xml);
    }

    [ExcludeFromCodeCoverage]
    [TestMethod()]
    public void EnsureIsValidUnknownTest()
    {
        rsaKeySetting = new RsaKeySetting()
        {
            SourceType = RsaKeySetting.RsaKeyDataSource.Unknown
            , Source = string.Empty
        };

        var ex = Assert.ThrowsException<InvalidOperationException>(() => rsaKeySetting.EnsureIsValid());

        Assert.IsNotNull(ex);
        Assert.AreEqual("SourceType is unknown", ex.Message);
    }

    [ExcludeFromCodeCoverage]
    [TestMethod()]
    public void EnsureIsValidSourceEmptyStringTest()
    {
        rsaKeySetting = new RsaKeySetting()
        {
            SourceType = RsaKeySetting.RsaKeyDataSource.File
            ,
            Source = string.Empty
        };

        var ex = Assert.ThrowsException<InvalidOperationException>(() => rsaKeySetting.EnsureIsValid());

        Assert.IsNotNull(ex);
        Assert.AreEqual("Source is null or whitespace", ex.Message);
    }

}
