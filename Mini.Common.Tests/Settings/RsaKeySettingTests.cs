﻿using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Settings;

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
    public void RsaXmlTest()
    {
        var result = rsaKeySetting.RsaXml();

        Assert.AreEqual(string.Empty, result);
    }

    [TestMethod()]
    public void GetRsaSecurityKeyEnvVarPublicKeyTest()
    {
        Environment.SetEnvironmentVariable("SOME_PRIVATE_KEY", privateKeyXml);

        rsaKeySetting = new()
        {
            SourceType = RsaKeySetting.RsaKeyDataSource.EnvironmentVariable
            , Source = "SOME_PRIVATE_KEY"
        };

        RsaSecurityKey rsaSecurityKey = rsaKeySetting.GetRsaSecurityKey(false);

        Assert.AreEqual(rsaSecurityKey.PrivateKeyStatus, PrivateKeyStatus.DoesNotExist);
    }

    [TestMethod()]
    public void GetRsaSecurityKeyEnvVarPrivateKeyTest()
    {
        Environment.SetEnvironmentVariable("SOME_PRIVATE_KEY", privateKeyXml);

        rsaKeySetting = new()
        {
            SourceType = RsaKeySetting.RsaKeyDataSource.EnvironmentVariable
            ,
            Source = "SOME_PRIVATE_KEY"
        };

        RsaSecurityKey rsaSecurityKey = rsaKeySetting.GetRsaSecurityKey(true);

        Assert.AreEqual(rsaSecurityKey.PrivateKeyStatus, PrivateKeyStatus.Exists);
    }

    [TestMethod()]
    public void GetRsaSecurityKeyEnvVarNotExistsTest()
    {
        rsaKeySetting = new()
        {
            SourceType = RsaKeySetting.RsaKeyDataSource.EnvironmentVariable
            , Source = "MISSING_PRIVATE_KEY"
        };

        Assert.AreEqual(String.Empty, rsaKeySetting.RsaXml());
    }

    [TestMethod()]
    public void GetRsaSecurityKeyFileTest()
    {
        rsaKeySetting = new()
        {
            SourceType = RsaKeySetting.RsaKeyDataSource.File
            , Source = "_Data/UnitTest-PrivateKey.xml"
        };

        RsaSecurityKey rsaSecurityKey = rsaKeySetting.GetRsaSecurityKey(false);

        Assert.AreEqual(rsaSecurityKey.PrivateKeyStatus, PrivateKeyStatus.DoesNotExist);
    }

}
