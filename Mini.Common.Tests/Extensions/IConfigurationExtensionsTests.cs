using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Extensions;
using Mini.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini.Common.Extensions.Tests
{
    [TestClass()]
    public class IConfigurationExtensionsTests
    {
        private readonly Dictionary<string, string> configurationValues = new()
        {
            { "Application:Name", "someName" },
            { "Application:Version", "someVersion" },

            { "Jwt:Conso:Issuer", "https://localhost:5001" },
            { "Jwt:Conso:Audience", "mini-console-app" },
            { "Jwt:Conso:ExpirationMinutes", "180" },
            { "Jwt:ExampleApp:Issuer", "https://localhost:5002" },
            { "Jwt:ExampleApp:Audience", "example-app" },
            { "Jwt:ExampleApp:ExpirationMinutes", "120" }
        };

        private IConfigurationRoot configurationRoot = new ConfigurationBuilder().Build();

        [TestInitialize]
        public void BeforeEachTest()
        {
            configurationRoot = new ConfigurationBuilder()
                .AddInMemoryCollection(configurationValues)
                .Build();
        }

        [TestMethod()]
        public void GetSectionAsTest()
        {
            ApplicationSetting? applicationSetting = configurationRoot.GetSectionAs<ApplicationSetting>("Application");

            Assert.IsNotNull(applicationSetting);
        }

        [TestMethod()]
        public void GetSectionChildrenTest()
        {
            IEnumerable<IConfigurationSection>? jwtList = configurationRoot.GetSectionChildren("Jwt");

            Assert.IsNotNull(jwtList);
            Assert.AreEqual(2, jwtList.Count());
        }

        [TestMethod()]
        public void GetConfigurationSectionTest()
        {
            var configurationSection = configurationRoot.GetConfigurationSection("Application");

            Assert.IsNotNull(configurationSection);
            Assert.AreEqual<string>("Application", configurationSection.Key);
            Assert.AreEqual<string>("Application", configurationSection.Path);
        }

        [ExcludeFromCodeCoverage]
        [TestMethod()]
        public void GetConfigurationSectionNotExistingTest()
        {
            var ex = Assert.ThrowsException<InvalidOperationException>(() => 
                configurationRoot.GetConfigurationSection("Application1"));

            Assert.IsNotNull(ex);
            Assert.AreEqual("Configuration section Application1 does not exists.", ex.Message);
        }
    }

}