using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Wms.MongoDbImplementation.Models;
using Mini.Wms.MongoDbImplementation.Services;
using MongoDB.Driver;
using Moq;

namespace Mini.Wms.MongoDbImplementation.Tests.Integration.Services;

[TestCategory("Integration")]
[TestClass()]
public class UserServiceTests
{
    private readonly IConfigurationRoot? config = new ConfigurationBuilder()
        .AddUserSecrets(System.Reflection.Assembly.GetExecutingAssembly())
        .AddJsonFile("appsettings.json")
        .Build();
    private readonly string miniToolsConnectionString;
    private readonly string databaseName;

    private Mock<ILogger<UserService>> mockLogger = new();

    private readonly IMongoClient mongoClient;
    private readonly IMongoDatabase database;
    private IMongoCollection<User> userCollection;
    private UserService? userService;

    public UserServiceTests()
    {
        miniToolsConnectionString = config["mongodb:minitools:ConnectionString"];
        databaseName = MongoUrl.Create(miniToolsConnectionString).DatabaseName;

        mongoClient = new MongoClient(miniToolsConnectionString);
        database = mongoClient.GetDatabase(databaseName);
        userCollection = database.GetCollection<User>(typeof(User).Name);
    }

    [TestInitialize]
    public void BeforeEachTest()
    {
        mockLogger = new Mock<ILogger<UserService>>();
        userCollection = database.GetCollection<User>(typeof(User).Name);
    }

    [TestMethod()]
    public void UserServiceTest()
    {
        userService = new UserService(mockLogger.Object, userCollection);

        Assert.IsNotNull(userService);
    }

    [TestMethod()]
    public async Task AddAsyncTestAsync()
    {
        userService = new UserService(mockLogger.Object, userCollection);

        User newUser = new()
        {
            Username = "someUsername",
            FirstName = "someFirstName",
            LastName = "someLastName"
        };

        await userService.AddAsync(newUser);

        Assert.IsNotNull(newUser.Id);
    }


    [TestMethod()]
    public async Task AllAsyncTestAsync()
    {
        userService = new UserService(mockLogger.Object, userCollection);

        var result = await userService.AllAsync();

        Assert.IsNotNull(result);
    }

    [TestMethod()]
    public async Task DeleteAsyncTestAsync()
    {
        userService = new UserService(mockLogger.Object, userCollection);

        var result = await userService.DeleteAsync("000000000000000000000000");

        Assert.IsTrue(result);
    }

    [TestMethod()]
    public async Task GetAsyncTestAsync()
    {
        userService = new UserService(mockLogger.Object, userCollection);

        var result = await userService.GetAsync("000000000000000000000000");

        Assert.IsNotNull(result);
        Assert.AreEqual("someUsername", result.Username);
    }

    [TestMethod()]
    public async Task UpdateAsyncTestAsync()
    {
        userService = new UserService(mockLogger.Object, userCollection);

        User record = new()
        {
            Id = "000000000000000000000000",
            Username = "someUsername",
            FirstName = "chgFirstName",
            LastName = "chgLastName"
        };

        var result = await userService.UpdateAsync(record);

        Assert.IsNotNull(result);
        Assert.AreEqual("someUsername", result.Username);
        Assert.AreEqual("chgFirstName", result.FirstName);
        Assert.AreEqual("chgLastName", result.LastName);
    }

}
