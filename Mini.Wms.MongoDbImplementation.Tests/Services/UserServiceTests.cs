using System;
using System.Collections.Generic;
using System.Threading;
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

[TestClass()]
public class UserServiceTests
{

    private Mock<ILogger<UserService>> mockLogger = new Mock<ILogger<UserService>>();
    private Mock<IMongoCollection<User>> mockUserCollection = new Mock<IMongoCollection<User>>();

    [TestInitialize]
    public void BeforeEachTest()
    {

    }

    [TestMethod()]
    public void UserServiceTest()
    {
        UserService userService = new UserService(mockLogger.Object, mockUserCollection.Object);

        Assert.IsNotNull(userService);
    }

    [TestMethod()]
    public async Task AddAsyncTestAsync()
    {
        UserService userService = new UserService(mockLogger.Object, mockUserCollection.Object);

        User record = new User()
        {
            Username = "someUsername",
            FirstName = "someFirstName",
            LastName = "someLastName"
        };

        await userService.AddAsync(record);

        mockUserCollection.Verify(m => m.InsertOneAsync(It.IsAny<User>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>())
        , Times.Once());
    }

    [TestMethod()]
    public async Task AllAsyncTestAsync()
    {
        Mock<IAsyncCursor<User>> mockCursor = new Mock<IAsyncCursor<User>>();

        //mockCursor.Setup(m => m.ToListAsync(It.IsAny<CancellationToken>()))
        //    .ReturnsAsync(new List<User>());
        //Mock<IAsyncCursor<Book>> _bookCursor = new Mock<IAsyncCursor<Book>>();
        mockCursor.Setup(m => m.Current).Returns(new List<User>());
        //mockCursor
        //    .SetupSequence(m => m.MoveNext(It.IsAny<CancellationToken>()))
        //    .Returns(true)
        //    .Returns(false);
        mockCursor.SetupSequence(m => m.MoveNextAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true))
            .Returns(Task.FromResult(false));


        //mockUserCollection.Setup(m => m.Find)
        mockUserCollection.Setup(m => m.FindAsync(It.IsAny<FilterDefinition<User>>(),
            It.IsAny<FindOptions<User, User>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        UserService userService = new UserService(mockLogger.Object, mockUserCollection.Object);

        var result = await userService.AllAsync();

        Assert.IsNotNull(result);
    }

    [TestMethod()]
    public async Task DeleteAsyncTestAsync()
    {
        mockUserCollection.Setup(m => m.DeleteOneAsync(It.IsAny<FilterDefinition<User>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DeleteResult.Acknowledged(1));

        UserService userService = new UserService(mockLogger.Object, mockUserCollection.Object);

        bool result = await userService.DeleteAsync("asd");

        Assert.IsTrue(result);
    }

    [TestMethod()]
    public async Task GetAsyncTestAsync()
    {
        Mock<IAsyncCursor<User>> mockCursor = new Mock<IAsyncCursor<User>>();

        //mockCursor.Setup(m => m.FirstOrDefaultAsync(It.IsAny<CancellationToken>()))
        //    .ReturnsAsync(new User());
        mockCursor.Setup(m => m.Current).Returns(new List<User> { new User(), new User() });
        //mockCursor
        //    .SetupSequence(m => m.MoveNext(It.IsAny<CancellationToken>()))
        //    .Returns(true)
        //    .Returns(false);
        mockCursor.SetupSequence(m => m.MoveNextAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(true))
            .Returns(Task.FromResult(false));

        //mockUserCollection.Setup(m => m.Find)
        mockUserCollection.Setup(m => m.FindAsync(It.IsAny<FilterDefinition<User>>(),
            It.IsAny<FindOptions<User, User>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

        //mockUserCollection.Setup(m => m.FindAsync(It.IsAny<FilterDefinition<User>>(), It.IsAny<FindOptions<User, User>>(), It.IsAny<CancellationToken>()))
        //    .ReturnsAsync(new )

        UserService userService = new UserService(mockLogger.Object, mockUserCollection.Object);

        var result = await userService.GetAsync("asd");

        Assert.IsNotNull(result);

    }

    [TestMethod()]
    public async Task UpdateAsyncTestAsync()
    {
        mockUserCollection.Setup(m => m.FindOneAndUpdateAsync(It.IsAny<FilterDefinition<User>>(),
            It.IsAny<UpdateDefinition<User>>(),
            It.IsAny<FindOneAndUpdateOptions<User, User>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(new User
            {
            });

        UserService userService = new UserService(mockLogger.Object, mockUserCollection.Object);

        User record = new User()
        {
            Id="asd",
            Username = "someUsername",
            FirstName = "someFirstName",
            LastName = "someLastName"
        };

        var result = await userService.UpdateAsync(record);

        Assert.IsNotNull(result);
    }
}

//namespace Mini.Wms.MongoDbImplementation.Tests.Services;


//[TestClass()]
//public class UserServiceTests
//{
//    private readonly IConfigurationRoot? config = new ConfigurationBuilder()
//        .AddUserSecrets(System.Reflection.Assembly.GetExecutingAssembly())
//        .AddJsonFile("appsettings.json")
//        .Build();

//    private Mock<ILogger<UserService>> mockLogger = new Mock<ILogger<UserService>>();

//    private IMongoClient mongoClient;
//    private IMongoDatabase database;
//    private IMongoCollection<User> userCollection;
//    private UserService? userService;

//    public UserServiceTests()
//    {
//        string miniToolsConnectionString = config["mongodb:minitools:ConnectionString"];
//        string databaseName = MongoUrl.Create(miniToolsConnectionString).DatabaseName;

//        mongoClient = new MongoClient(miniToolsConnectionString);
//        database = mongoClient.GetDatabase(databaseName);
//        userCollection = database.GetCollection<User>(typeof(User).Name);
//    }

//    [TestInitialize]
//    public void BeforeEachTest()
//    {
//        mockLogger = new Mock<ILogger<UserService>>();

//        string miniToolsConnectionString = config["mongodb:minitools:ConnectionString"];

//        string databaseName = MongoUrl.Create(miniToolsConnectionString).DatabaseName;

//        mongoClient = new MongoClient(miniToolsConnectionString);
//        database = mongoClient.GetDatabase(databaseName);
//        userCollection = database.GetCollection<User>(typeof(User).Name);

//    }


//    [TestMethod()]
//    public void UserServiceTest()
//    {
//        userService = new UserService(mockLogger.Object, userCollection);

//        Assert.IsNotNull(userService);
//    }

//    [TestMethod()]
//    public async Task AddAsyncTestAsync()
//    {
//        userService = new UserService(mockLogger.Object, userCollection);

//        //IUser<string> data = new 
//        User record = new User()
//        {
//            Username = "someUsername3",
//            FirstName = "someFirstName3",
//            LastName = "someLastName3"
//        };

//        await userService.AddAsync(record);

//        Assert.IsNotNull(record.Id);
//    }

//    //[TestMethod()]
//    //public void AllAsyncTest()
//    //{
//    //    Assert.Fail();
//    //}

//    //[TestMethod()]
//    //public void DeleteAsyncTest()
//    //{
//    //    Assert.Fail();
//    //}

//    //[TestMethod()]
//    //public void DeleteAsyncTest1()
//    //{
//    //    Assert.Fail();
//    //}

//    //[TestMethod()]
//    //public void GetAsyncTest()
//    //{
//    //    Assert.Fail();
//    //}

//    //[TestMethod()]
//    //public void UpdateAsyncTest()
//    //{
//    //    Assert.Fail();
//    //}
//}
