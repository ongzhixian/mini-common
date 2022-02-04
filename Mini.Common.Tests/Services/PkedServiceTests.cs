using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Models;
using Mini.Common.Requests;
using Mini.Common.Services;
using Mini.Common.Settings;
using Moq;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mini.Common.Tests.Services
{
    [TestClass()]
    public class PkedServiceTests
    {
        private Mock<IOptionsSnapshot<RsaKeySetting>> rsaKeySettingOptions = new();

        private readonly string encryptingKeyPublicXml = @"
<RSAKeyValue>
<Modulus>1GRNR0E6q83gpXWMsS8Np5AkjcIu8zFmP4LYxwrJDSiftI2DolNyRkf1fPdC6BnEWgL9ZDkGwbS8uPvqSuOfat5NpAoozsDoGbnqnsgxS4f9esIX01HlD1FElPEZEJMlrMYdUdo5kOSG6chNPIk8MLJxoCoSDGf98KYr+CmREw6itUkULELK0RcqRkYaJfe636Koi4cUbICfqsLvOnnJYsm8GNIQbm4tjrxt/lYGsyGHAE2bvDYlSV9rPml/AM7uNc74MW6Xs9Vxs2gIybttC6NfS/i9o+whi4+kRnQUIonMIVgSGTdQO2CitoZ+jswaPDH8g6JmvKmTyTHZaIppyQ==</Modulus>
<Exponent>AQAB</Exponent>
</RSAKeyValue>";

        [TestInitialize]
        public void BeforeEachTest()
        {
            rsaKeySettingOptions = new Mock<IOptionsSnapshot<RsaKeySetting>>();
        }

        [TestMethod()]
        public void PkedServiceTest()
        {
            PkedService pkedService = new(rsaKeySettingOptions.Object);

            Assert.IsNotNull(pkedService);
        }

        [TestMethod()]
        public async Task EncryptAsyncTestAsync()
        {
            Environment.SetEnvironmentVariable("TEST_ENCRYPT_PUBLIC_KEY",
                @"<RSAKeyValue><Modulus>nynqKp7ayQ2fubvjdG2RnVN2NHwDVphSOeRV4h0d8vXhZLX3z7YfSfQYnDtkudqUr4ZJBnCnZuudZmCCX4hoGGDkC8DeA8GGi8wzMMOdyi8t/chYidgl3MX44xYdl2YslncAcUaRtrpVrY9/ZLc2EnPvI3xwSZUdLcjSc9myi46ZnfuZ87TRFHvhGyQIDvUaOfxrB/+5C3VutgNsUHFAbwsvCWFyMMXjXnxpLfkOONi+DVgf02mvblqRKWFqUDnjM2062RhlXRCg9dJKSsknyIF8/dPzCwW9aEG9IqdNWzpXfqIghwYiXt42PQhNcCiLboRGMvKJC4V3Dp4DUIVyQQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");

            RsaKeySetting recepRsaKeySetting = new()
            {
                SourceType = RsaKeySetting.RsaKeyDataSource.EnvironmentVariable,
                Source = "TEST_ENCRYPT_PUBLIC_KEY"
            };

            rsaKeySettingOptions.Setup(m => m.Get("someEncryptionKeyName")).Returns(recepRsaKeySetting);

            PkedService pkedService = new(rsaKeySettingOptions.Object);

            EncryptedMessage result = await pkedService.EncryptAsync("someData", "someEncryptionKeyName");

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task DecryptAsyncTestAsync()
        {
            Environment.SetEnvironmentVariable("TEST_DECRYPT_PRIVATE_KEY",
                @"<RSAKeyValue><Modulus>nynqKp7ayQ2fubvjdG2RnVN2NHwDVphSOeRV4h0d8vXhZLX3z7YfSfQYnDtkudqUr4ZJBnCnZuudZmCCX4hoGGDkC8DeA8GGi8wzMMOdyi8t/chYidgl3MX44xYdl2YslncAcUaRtrpVrY9/ZLc2EnPvI3xwSZUdLcjSc9myi46ZnfuZ87TRFHvhGyQIDvUaOfxrB/+5C3VutgNsUHFAbwsvCWFyMMXjXnxpLfkOONi+DVgf02mvblqRKWFqUDnjM2062RhlXRCg9dJKSsknyIF8/dPzCwW9aEG9IqdNWzpXfqIghwYiXt42PQhNcCiLboRGMvKJC4V3Dp4DUIVyQQ==</Modulus><Exponent>AQAB</Exponent><P>z+H/c0SjTBC7BLnRj5kXQEUi71CPh7m0OsyEQ4Oxdhmu9Yd9Eg8KFhJixeuhTFTX48lFcIJQynqTAJ5h3MwRYb/B4K6rOTzawrEYkrcsHdykIr2mlX/Tz6AFVb9UxqIJLyT6/QR+YdOjKj8m3qB1l5WpP98QNcqytc0G/shSZ5s=</P><Q>xAEXCLOY3S34ktGrfZ+Q7xgxiStWw+tpv0Od6W8FsChz1VZUJPVLFxAaLalYGmvR3YHstRo0/6+gQT9fxNlEzRVEdDnoAqYQ93e3Vz5ga+Ju7EymbOticLlKSLBbL7yax32itxvM6wbO6bKB+aUyLbQYlT6ACd98fsR5atk2wVM=</Q><DP>DxEkQKiC5Wp5A5isNao04zWfmJTQMlglVfWoyHPtEtA9vBH74KkyY/MlMY8oe63Sj/HwlHz9eg90RCE2mpg30kvhs3U1d8qPWHkZfNjF28w4IUR5pHBFW9EPUe2yKUtpOv+FKX1/43tBYjh9irk6jKgOQlDajeuJFGAt152RS20=</DP><DQ>WJtRiWp/cYXFuMG2b/0BrUTXp/YCrGExFfkAWzYIcUFoVApJ0cdeIdfyra+/l3okjqmck6Z8TRZdMQHwTnIWK0ww/QXsf74JL4ZcSMF3H25mzMY9+kFS6Dirfz7SijsOEZ8XPwX/VkRmp7k+DtiCXazr7BQsF8qDYl95sDbwDOc=</DQ><InverseQ>ulmWEKVCRwiRzboaSGZF9bGBPE9eGSEYoxdMbsd1/4H9anFiY9D62SvddR+tUqNc4eF6SisZIrcldt/aIeDNxwz8UQnqlH/jt6BQjozyijWCe2WRAX8rbGMrx9JOsOZuQc7oyo6pnvbmM8cojPuyFgdAsJz7EyMqUtjCTEjK5Zc=</InverseQ><D>bdYU/LiHLvux4BCjFHOQa0tvZSS3gQi4pa7G/bUuN74qfr98deCukvBaE2aBSihnNwGqX0FapJGwGzg6gAa4Hyqt9NZx35GrccnHAb3co+wPBvU89FMvPb141e0rGpGt71/L9F/IqprICW/fb/dm3zEWNBzpuD2o1zGg2cW/isIU1DbMi/9lgwgmi3Wl4EFQSlmz7a6Wamur2dUpT1rgOWFdzxpHW5GkkJ7n3l9+iFeUob5nf32hd3jRJdTsO9HLZOIJiAO87ceNZqhfJjMgtE16nvXeE8qx+KBpQwOhCcoUqYB79iC0vB+LQwxq5JvdIPz14NXk4cEPPRwyNClxyQ==</D></RSAKeyValue>");

            EncryptedMessage encryptedMessage = JsonSerializer.Deserialize<EncryptedMessage>(
                "{\"IV\":\"AV+qQoGj8FKOYlzUm9XKUg==\",\"EncryptedSessionKey\":\"ezFx+HC59WZ+4AtsZ3KVxHrNX422Ng9HO9wm7C9LqIdariJzL0rcpg4ef2RhZg8YyB2y9rIUEqGUy3j0o1EK6Tt2um92zKFNJRpAOR5q6lz7HpZCB5HdoIpt3L9711MJdy0c51MKM5pQpMCtjLYxWkfDtAJz7FKKN+h+WFPSl3BRt3j3phCqkNm494Nr0i2UiH74UCONweFlzmlE02bjrM9YvU7gUJezCp4ckte1c0mEw7m48rVNBNIGDtdPi0jrbLl2lTUpfJbX/KrknxTpXgf8uY6Q1Av6uXIA9RWxizZ1tbSb//a68tvbZcaJRq886hLWHxXqVy+dESVviaasBQ==\",\"EncryptedMessageBytes\":\"xI7IfMvuy0n1ETr+tJjNTQ==\"}");

            RsaKeySetting recepRsaKeySetting = new()
            {
                SourceType = RsaKeySetting.RsaKeyDataSource.EnvironmentVariable,
                Source = "TEST_DECRYPT_PRIVATE_KEY"
            };

            rsaKeySettingOptions.Setup(m => m.Get("someEncryptionKeyName")).Returns(recepRsaKeySetting);

            PkedService pkedService = new(rsaKeySettingOptions.Object);

            string? result = await pkedService.DecryptAsync<string>(encryptedMessage, "someEncryptionKeyName");

            Assert.IsNotNull(result);
            Assert.AreEqual("someData", result);
        }

        [TestMethod()]
        public async Task EncryptAsyncLoginRequestTestAsync()
        {
            Environment.SetEnvironmentVariable("TEST_ENCRYPT_PUBLIC_KEY",
                @"<RSAKeyValue><Modulus>nynqKp7ayQ2fubvjdG2RnVN2NHwDVphSOeRV4h0d8vXhZLX3z7YfSfQYnDtkudqUr4ZJBnCnZuudZmCCX4hoGGDkC8DeA8GGi8wzMMOdyi8t/chYidgl3MX44xYdl2YslncAcUaRtrpVrY9/ZLc2EnPvI3xwSZUdLcjSc9myi46ZnfuZ87TRFHvhGyQIDvUaOfxrB/+5C3VutgNsUHFAbwsvCWFyMMXjXnxpLfkOONi+DVgf02mvblqRKWFqUDnjM2062RhlXRCg9dJKSsknyIF8/dPzCwW9aEG9IqdNWzpXfqIghwYiXt42PQhNcCiLboRGMvKJC4V3Dp4DUIVyQQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");

            RsaKeySetting recepRsaKeySetting = new()
            {
                SourceType = RsaKeySetting.RsaKeyDataSource.EnvironmentVariable,
                Source = "TEST_ENCRYPT_PUBLIC_KEY"
            };

            rsaKeySettingOptions.Setup(m => m.Get("someEncryptionKeyName")).Returns(recepRsaKeySetting);

            PkedService pkedService = new(rsaKeySettingOptions.Object);

            EncryptedMessage result = await pkedService.EncryptAsync(new LoginRequest()
            {
                Username = "someUsername",
                Password = "somePassword",
                Encrypting = new SecurityCredential
                {
                    SecurityAlgorithm = SecurityAlgorithms.RsaOAEP,
                    SecurityDigest = SecurityAlgorithms.Aes256CbcHmacSha512,
                    Xml = encryptingKeyPublicXml
                }
            }, "someEncryptionKeyName");

            Assert.IsNotNull(result);
        }
    }
}