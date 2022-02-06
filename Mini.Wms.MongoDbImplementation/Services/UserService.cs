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
        this.userCollection = userCollection ?? throw new ArgumentException(nameof(userCollection));
    }



    public IEnumerable<IUser<T>> All(CancellationToken cancellationToken = default)
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
        //return userCollection.Find(Builders<IUser<T>>.Filter.Empty).ToList();

    }



    public async Task<IUser<T>> GetAsync(IUser<T> id
        , FindOptions<IUser<T>, IUser<T>> findOptions
        , CancellationToken cancellationToken = default)
    {
        FilterDefinition<IUser<T>> filter = Builders<IUser<T>>.Filter.Eq("_id", id);
        
        //var findOptions = new FindOptions<IUser<T>, IUser<T>>();

        var cursor = await userCollection.FindAsync(filter, findOptions, cancellationToken);

        var user = await cursor.FirstOrDefaultAsync();

        return user;
    }

    public async Task<DeleteResult> DeleteAsync(IUser<T> user
    , CancellationToken cancellationToken = default)
    {
        FilterDefinition<IUser<T>> filter = Builders<IUser<T>>.Filter.Eq("_id", user.Id);
        
        DeleteResult? deleteResult = await userCollection.DeleteOneAsync(filter, cancellationToken);

        return deleteResult;
    }


    public async Task<IUser<T>> UpdateAsync(IUser<T> user
        , FilterDefinition<IUser<T>> filterDefinition
        , UpdateDefinition<IUser<T>> updateDefinition
        , FindOneAndUpdateOptions<IUser<T>, IUser<T>> findOneAndUpdateOptions
        , CancellationToken cancellationToken = default)
    {
        var filter = Builders<IUser<T>>.Filter.Eq(r => r.Id, user.Id);
        var update = Builders<IUser<T>>.Update
            .Set(r => r.FirstName, user.FirstName)
            .Set(r => r.LastName, user.LastName);

        var options = new FindOneAndUpdateOptions<IUser<T>, IUser<T>>()
        {
        };

        var updateResult = await userCollection.FindOneAndUpdateAsync(
            filterDefinition
            , updateDefinition
            , findOneAndUpdateOptions
            , cancellationToken);

        return updateResult;

    }

    public async Task AddAsync(IUser<T> user
        , InsertOneOptions? insertOneOptions = null
        , CancellationToken cancellationToken = default)
    {
        await userCollection.InsertOneAsync(user, insertOneOptions, cancellationToken);
    }

    //void IObjectService<IUser<T>>.AddAsync(IUser<T> user, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task AddAsync(T user, CancellationToken cancellationToken = default)
    //{
    //    InsertOneOptions option = new InsertOneOptions
    //    {

    //    };

    //    await userCollection.InsertOneAsync(user, option, cancellationToken);
    //}


}