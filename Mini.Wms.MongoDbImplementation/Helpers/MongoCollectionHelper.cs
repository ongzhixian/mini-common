using MongoDB.Driver;

namespace Mini.Wms.MongoDbImplementation.Helpers;

public static class MongoCollectionHelper
{
    public static async Task<string> CreateUniqueIndexAsync<T>(string fieldName
        , IMongoCollection<T> mongoCollection
        , CreateOneIndexOptions? createOneIndexOptions = null
        , CancellationToken cancellationToken = default)
    {
        var options = new CreateIndexOptions() { Unique = true, Name = "uniqueIndex" };

        var field = new StringFieldDefinition<T>(fieldName);
        
        var indexDefinition = new IndexKeysDefinitionBuilder<T>().Ascending(field);

        CreateIndexModel<T> createIndexModel = new CreateIndexModel<T>(indexDefinition, options);

        string nameOfIndex = await mongoCollection.Indexes.CreateOneAsync(createIndexModel, createOneIndexOptions, cancellationToken);

        return nameOfIndex;
    }
}
