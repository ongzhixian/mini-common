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

    private Mock<ILogger<UserService>> mockLogger = new Mock<ILogger<UserService>>();

    private IMongoClient mongoClient;
    private IMongoDatabase database;
    private IMongoCollection<User> userCollection;
    private UserService? userService;

    public UserServiceTests()
    {
        string miniToolsConnectionString = config["mongodb:minitools:ConnectionString"];
        string databaseName = MongoUrl.Create(miniToolsConnectionString).DatabaseName;

        mongoClient = new MongoClient(miniToolsConnectionString);
        database = mongoClient.GetDatabase(databaseName);
        userCollection = database.GetCollection<User>(typeof(User).Name);
    }

    //[ClassInitialize]
    //public async Task BeforeAllTestAsync()
    //{
    //    //var options = new CreateIndexOptions() { Unique = true };
    //    //var field = new StringFieldDefinition<User>("Username");
    //    //var indexDefinition = new IndexKeysDefinitionBuilder<User>().Ascending(field);
    //    //CreateIndexModel<User> createIndexModel = new CreateIndexModel<User>(indexDefinition, options);
    //    //var x = await userCollection.Indexes.CreateOneAsync(createIndexModel);
    //}

    [TestInitialize]
    public void BeforeEachTest()
    {
        mockLogger = new Mock<ILogger<UserService>>();

        string miniToolsConnectionString = config["mongodb:minitools:ConnectionString"];

        string databaseName = MongoUrl.Create(miniToolsConnectionString).DatabaseName;

        mongoClient = new MongoClient(miniToolsConnectionString);
        database = mongoClient.GetDatabase(databaseName);
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

        // 6201bfe6be1eb4cc604fd8be
        // 000000000000000000000000
        //var oid = new MongoDB.Bson.ObjectId("000000000000000000000000");

        User record = new User()
        {
            
            Id = "000000000000000000000000",
            Username = "someUsername5",
            FirstName = "someFirstName3",
            LastName = "someLastName3"
        };

        await userService.AddAsync(record);

        Assert.IsNotNull(record.Id);
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

        User record = new User()
        {
            Id = "000000000000000000000000",
            Username = "someUsername3",
            FirstName = "chgFirstName4",
            LastName = "chgLastName4"
        };

        var result = await userService.UpdateAsync(record);

        Assert.IsNotNull(result);
        Assert.AreEqual("someUsername", result.Username);
    }

}
