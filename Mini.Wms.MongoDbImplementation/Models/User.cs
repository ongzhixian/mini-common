using System.Text.Json.Serialization;
using Mini.Wms.Abstraction.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mini.Wms.MongoDbImplementation.Models;

public readonly record struct User : IUser<string>
{
    public string Username { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public DateTime PasswordUpdatedDateTime { get; init; } = DateTime.MinValue;

    [JsonPropertyName("id")]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; } = string.Empty;

    //[JsonPropertyName("createdDateTime")]
    //[BsonElement("createdDateTime")]
    public DateTime CreatedDateTime { get; init; } = DateTime.MinValue;

    //[JsonPropertyName("lastUpdatedDateTime")]
    //[BsonElement("lastUpdatedDateTime")]
    public DateTime LastUpdatedDateTime { get; init; } = DateTime.MinValue;
}
