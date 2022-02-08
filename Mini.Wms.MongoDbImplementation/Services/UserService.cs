﻿using Microsoft.Extensions.Logging;
using Mini.Wms.Abstraction.Services;
using Mini.Wms.MongoDbImplementation.Models;
using MongoDB.Driver;

namespace Mini.Wms.MongoDbImplementation.Services;

public class UserService : IUserService<string, User>
{
    private readonly ILogger<UserService> logger;
    private readonly IMongoCollection<User> userCollection;

    public UserService(ILogger<UserService> logger, IMongoCollection<User> userCollection)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.userCollection = userCollection ?? throw new ArgumentNullException(nameof(userCollection));
    }


    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        InsertOneOptions? insertOneOptions = null;

        await userCollection.InsertOneAsync(user, insertOneOptions, cancellationToken);

    }

    public async Task<IEnumerable<User>> AllAsync(CancellationToken cancellationToken = default)
    {
        var cursor = await userCollection.FindAsync(Builders<User>.Filter.Empty, null, cancellationToken);


        return await cursor.ToListAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("_id", id);

        DeleteResult? deleteResult = await userCollection.DeleteOneAsync(filter, cancellationToken);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task<User> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("_id", id);

        var findOptions = new FindOptions<User, User>();

        var cursor = await userCollection.FindAsync(filter, findOptions, cancellationToken);

        var user = await cursor.FirstOrDefaultAsync(cancellationToken);

        return user;
    }

    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<User>.Filter.Eq(r => r.Id, user.Id);
        var updateDefinition = Builders<User>.Update
            .Set(r => r.FirstName, user.FirstName)
            .Set(r => r.LastName, user.LastName);

        var findOneAndUpdateOptions = new FindOneAndUpdateOptions<User, User>()
        {
        };

        var updateResult = await userCollection.FindOneAndUpdateAsync(
            filterDefinition
            , updateDefinition
            , findOneAndUpdateOptions
            , cancellationToken);

        return updateResult;
    }
}
