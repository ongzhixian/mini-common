using System.Text.Json.Serialization;
using Mini.Wms.Abstraction.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mini.Wms.MongoDbImplementation.Models;

// Note: MongoDb has trouble deserializing structs and interface types; so using record class instead.
// See: https://stackoverflow.com/questions/14561809/deserialize-object-as-an-interface-with-mongodb-c-sharp-driver


public record class User : IUser<string>
{
    public string Username { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public DateTime PasswordUpdatedDateTime { get; init; } = DateTime.MinValue;

    //[JsonPropertyName("id")]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; } = null;

    //[JsonPropertyName("createdDateTime")]
    //[BsonElement("createdDateTime")]
    public DateTime CreatedDateTime { get; init; } = DateTime.MinValue;

    //[JsonPropertyName("lastUpdatedDateTime")]
    //[BsonElement("lastUpdatedDateTime")]
    public DateTime LastUpdatedDateTime { get; init; } = DateTime.MinValue;
}
