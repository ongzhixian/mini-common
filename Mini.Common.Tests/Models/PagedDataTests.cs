using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mini.Common.Models;

namespace Mini.Common.Tests.Models;

[TestClass()]
public class PagedDataTests
{
    private readonly record struct UserRecord(string Username, string FirstName, string LastName);

    private PagedData<UserRecord> testData = new();

    [TestMethod()]
    public void SerializeTest()
    {
        testData = new PagedData<UserRecord>()
        {
            TotalRecordCount = 0,
            Page = 1,
            PageSize = 10,
            Data = new List<UserRecord>
            {
                new UserRecord { FirstName = "fname1", LastName = "lname1", Username = "uname1"}
                , new UserRecord { FirstName = "fname2", LastName = "lname2", Username = "uname2"}
                , new UserRecord { FirstName = "fname3", LastName = "lname3", Username = "uname3"}
                , new UserRecord { FirstName = "fname4", LastName = "lname4", Username = "uname4"}
                , new UserRecord { FirstName = "fname5", LastName = "lname5", Username = "uname5"}
            }
        };

        var json = JsonSerializer.Serialize(testData);

        Assert.AreEqual("{\"Page\":1,\"PageSize\":10,\"TotalRecordCount\":0,\"Data\":[{\"Username\":\"uname1\",\"FirstName\":\"fname1\",\"LastName\":\"lname1\"},{\"Username\":\"uname2\",\"FirstName\":\"fname2\",\"LastName\":\"lname2\"},{\"Username\":\"uname3\",\"FirstName\":\"fname3\",\"LastName\":\"lname3\"},{\"Username\":\"uname4\",\"FirstName\":\"fname4\",\"LastName\":\"lname4\"},{\"Username\":\"uname5\",\"FirstName\":\"fname5\",\"LastName\":\"lname5\"}]}", json);
    }

    [TestMethod()]
    public void DeserializeTest()
    {
        string json = "{\"Page\":1,\"PageSize\":10,\"TotalRecordCount\":0,\"Data\":[{\"Username\":\"uname1\",\"FirstName\":\"fname1\",\"LastName\":\"lname1\"},{\"Username\":\"uname2\",\"FirstName\":\"fname2\",\"LastName\":\"lname2\"},{\"Username\":\"uname3\",\"FirstName\":\"fname3\",\"LastName\":\"lname3\"},{\"Username\":\"uname4\",\"FirstName\":\"fname4\",\"LastName\":\"lname4\"},{\"Username\":\"uname5\",\"FirstName\":\"fname5\",\"LastName\":\"lname5\"}]}";

        testData = JsonSerializer.Deserialize<PagedData<UserRecord>>(json);

        Assert.AreEqual((uint)1, testData.Page);
        Assert.AreEqual((uint)10, testData.PageSize);
        Assert.AreEqual((ulong)0, testData.TotalRecordCount);
        Assert.AreEqual(5, testData.Data.Count());
    }


    [TestMethod()]
    public void SerializeUsingWebTest()
    {
        testData = new PagedData<UserRecord>()
        {
            TotalRecordCount = 0,
            Page = 1,
            PageSize = 10,
            Data = new List<UserRecord>
            {
                new UserRecord { FirstName = "fname1", LastName = "lname1", Username = "uname1"}
                , new UserRecord { FirstName = "fname2", LastName = "lname2", Username = "uname2"}
                , new UserRecord { FirstName = "fname3", LastName = "lname3", Username = "uname3"}
                , new UserRecord { FirstName = "fname4", LastName = "lname4", Username = "uname4"}
                , new UserRecord { FirstName = "fname5", LastName = "lname5", Username = "uname5"}
            }
        };

        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var json = JsonSerializer.Serialize(testData, options);

        Assert.AreEqual("{\"page\":1,\"pageSize\":10,\"totalRecordCount\":0,\"data\":[{\"username\":\"uname1\",\"firstName\":\"fname1\",\"lastName\":\"lname1\"},{\"username\":\"uname2\",\"firstName\":\"fname2\",\"lastName\":\"lname2\"},{\"username\":\"uname3\",\"firstName\":\"fname3\",\"lastName\":\"lname3\"},{\"username\":\"uname4\",\"firstName\":\"fname4\",\"lastName\":\"lname4\"},{\"username\":\"uname5\",\"firstName\":\"fname5\",\"lastName\":\"lname5\"}]}", json);
    }

    [TestMethod()]
    public void DeserializeUsingWebTest()
    {
        string json = "{\"page\":1,\"pageSize\":10,\"totalRecordCount\":0,\"data\":[{\"username\":\"uname1\",\"firstName\":\"fname1\",\"lastName\":\"lname1\"},{\"username\":\"uname2\",\"firstName\":\"fname2\",\"lastName\":\"lname2\"},{\"username\":\"uname3\",\"firstName\":\"fname3\",\"lastName\":\"lname3\"},{\"username\":\"uname4\",\"firstName\":\"fname4\",\"lastName\":\"lname4\"},{\"username\":\"uname5\",\"firstName\":\"fname5\",\"lastName\":\"lname5\"}]}";

        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        testData = JsonSerializer.Deserialize<PagedData<UserRecord>>(json, options);

        Assert.AreEqual((uint)1, testData.Page);
        Assert.AreEqual((uint)10, testData.PageSize);
        Assert.AreEqual((ulong)0, testData.TotalRecordCount);
        Assert.AreEqual(5, testData.Data.Count());
    }
}
