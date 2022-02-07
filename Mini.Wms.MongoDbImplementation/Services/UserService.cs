using Microsoft.Extensions.Logging;
using Mini.Wms.Abstraction.Models;
using Mini.Wms.Abstraction.Services;
using MongoDB.Driver;

namespace Mini.Wms.MongoDbImplementation.Services;

public class UserService<T> : IUserService<T>
{
    private readonly ILogger<UserService<T>> logger;
    private readonly IMongoCollection<IUser<T>> userCollection;

    public UserService(ILogger<UserService<T>> logger, IMongoCollection<IUser<T>> userCollection)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.userCollection = userCollection ?? throw new ArgumentNullException(nameof(userCollection));
    }

    public async Task<IUser<T>> AddAsync(IUser<T> user
    , CancellationToken cancellationToken = default)
    {
        InsertOneOptions? insertOneOptions = null;

        try
        {
            await userCollection.InsertOneAsync(user, insertOneOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            throw;
        }
        

        return user;
    }

    public async Task<IEnumerable<IUser<T>>> AllAsync(CancellationToken cancellationToken = default)
    {
        //FindOptions<IUser<T>> findOptions = null;

        //var options = new FindOptions<IUser<T>>()
        //{
        //    Projection = Builders<IUser<T>>.Projection
        //        .Include(p => p.Name)
        //        .Exclude(p => p.Id)
        //};

        //var x = await userCollection.FindAsync(
        //    Builders<IUser<T>>.Filter.Empty
        //    , findOptions
        //    , cancellationToken);

        var cursor = await userCollection.FindAsync(Builders<IUser<T>>.Filter.Empty, null, cancellationToken);

        return await cursor.ToListAsync(cancellationToken);

    }

    public async Task<bool> DeleteAsync(IUser<T> user
    , CancellationToken cancellationToken = default)
    {
        FilterDefinition<IUser<T>> filter = Builders<IUser<T>>.Filter.Eq("_id", user.Id);

        DeleteResult? deleteResult = await userCollection.DeleteOneAsync(filter, cancellationToken);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount <= 0;
    }

    public async Task<bool> DeleteAsync(T id, CancellationToken cancellationToken = default)
    {
        FilterDefinition<IUser<T>> filter = Builders<IUser<T>>.Filter.Eq("_id", id);

        DeleteResult? deleteResult = await userCollection.DeleteOneAsync(filter, cancellationToken);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount <= 0;
    }

    public async Task<IUser<T>> GetAsync(T id, CancellationToken cancellationToken = default)
    {
        FilterDefinition<IUser<T>> filter = Builders<IUser<T>>.Filter.Eq("_id", id);

        var findOptions = new FindOptions<IUser<T>, IUser<T>>();

        var cursor = await userCollection.FindAsync(filter, findOptions, cancellationToken);

        var user = await cursor.FirstOrDefaultAsync(cancellationToken);

        return user;
    }

    public async Task<IUser<T>> UpdateAsync(IUser<T> user
        , CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<IUser<T>>.Filter.Eq(r => r.Id, user.Id);
        var updateDefinition = Builders<IUser<T>>.Update
            .Set(r => r.FirstName, user.FirstName)
            .Set(r => r.LastName, user.LastName);

        var findOneAndUpdateOptions = new FindOneAndUpdateOptions<IUser<T>, IUser<T>>()
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
