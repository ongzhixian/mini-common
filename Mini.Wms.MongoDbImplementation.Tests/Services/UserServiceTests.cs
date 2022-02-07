using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Wms.Abstraction.Models;
using Mini.Wms.MongoDbImplementation.Models;
using Mini.Wms.MongoDbImplementation.Services;
using MongoDB.Driver;
using Moq;

namespace Mini.Wms.MongoDbImplementation.Tests.Services;


[TestCategory("Integration")]
[TestClass()]
public class UserServiceTests
{
    private IConfigurationRoot? config = new ConfigurationBuilder()
        .AddUserSecrets(System.Reflection.Assembly.GetExecutingAssembly())
        .AddJsonFile("appsettings.json")
        .Build();

    private IMongoClient mongoClient;
    private IMongoDatabase database;
    private IMongoCollection<IUser<string>> userCollection;

    private Mock<ILogger<UserService<string>>> mockLogger = new Mock<ILogger<UserService<string>>>();
    private UserService<string> userService;

    [TestInitialize]
    public void BeforeEachTest()
    {
        string miniToolsConnectionString = config["mongodb:minitools:ConnectionString"];
        string databaseName = MongoUrl.Create(miniToolsConnectionString).DatabaseName;

        mongoClient = new MongoClient(miniToolsConnectionString);
        database = mongoClient.GetDatabase(databaseName);
        userCollection = database.GetCollection<IUser<string>>(typeof(User).Name);

        mockLogger = new Mock<ILogger<UserService<string>>>();
    }

    
    [TestMethod()]
    public void UserServiceTest()
    {
        userService = new UserService<string>(mockLogger.Object, userCollection);

        Assert.IsNotNull(userService);
    }

    [TestMethod()]
    public async Task AddAsyncTestAsync()
    {
        userService = new UserService<string>(mockLogger.Object, userCollection);

        //IUser<string> data = new 
        User record = new User()
        {
            Username = "someUsername",
            FirstName = "someFirstName",
            LastName = "someLastName"
        };

        try
        {
            var result = await userService.AddAsync(record);
        }
        catch (System.Exception ex)
        {

            throw;
        }
        

        Assert.IsNotNull(userService);
        Assert.Fail();
    }

    //[TestMethod()]
    //public void AllAsyncTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod()]
    //public void DeleteAsyncTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod()]
    //public void DeleteAsyncTest1()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod()]
    //public void GetAsyncTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod()]
    //public void UpdateAsyncTest()
    //{
    //    Assert.Fail();
    //}
}
