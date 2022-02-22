using Microsoft.Extensions.Logging;
using Mini.Common.Models;
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
        FilterDefinition<User>? filter = Builders<User>.Filter.Exists("i");

        FilterDefinition<User>? fil = Builders<User>.Filter.Empty;
        var finalFilter = Builders<User>.Filter.And(filter, fil);


        var sort = Builders<User>.Sort.Descending("i");

        FindOptions<User, User> options = new FindOptions<User, User>();
        options.Sort = sort;
        options.Limit = 100;
        options.Skip = 0;
        

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

    public async Task<PagedData<User>> PageAsync(PagedDataOptions pagedDataOptions, CancellationToken cancellationToken = default)
    {
        FilterDefinition<User>? filter = Builders<User>.Filter.Exists("i");
        FilterDefinition<User>? fil = Builders<User>.Filter.Empty;
        var finalFilter = Builders<User>.Filter.And(filter, fil);

        IList<SortDefinition<User>> sortDefinitionList = new List<SortDefinition<User>>();
        
        //(pagedDataOptions.DataFieldList
        foreach (var field in pagedDataOptions.DataFieldList.OrderBy(r => r.SortOrder))
        {
            if (field.SortAscending)
            {
                sortDefinitionList.Add(Builders<User>.Sort.Ascending(field.Name));
            }
            else
            {
                sortDefinitionList.Add(Builders<User>.Sort.Descending(field.Name));
            }
        }

        FindOptions<User, User> options = new FindOptions<User, User>();
        options.Sort = Builders<User>.Sort.Combine(sortDefinitionList);
        options.Limit = (int)pagedDataOptions.PageSize;
        options.Skip = (int)((pagedDataOptions.Page - 1) * pagedDataOptions.PageSize);

        long documentCount = await userCollection.CountDocumentsAsync(Builders<User>.Filter.Empty, null, cancellationToken);

        var cursor = await userCollection.FindAsync(Builders<User>.Filter.Empty, options, cancellationToken);

        return new PagedData<User>(
            pagedDataOptions.Page,
            pagedDataOptions.PageSize,
            (ulong)documentCount,
            await cursor.ToListAsync(cancellationToken)
            );

        //PagedData<User> pagedData = new()
        //{
        //    TotalRecordCount = (ulong)documentCount,
        //    Data = await cursor.ToListAsync(cancellationToken),
        //    Page = pagedDataOptions.Page,
        //    PageSize = pagedDataOptions.PageSize
        //};

        //return pagedData;

        //return await cursor.ToListAsync(cancellationToken);
    }

}

//public class PagedData<User>
//{
//    public long TotalRecordCount { get; internal set; }

//    public List<User> Data { get; internal set; }
//}