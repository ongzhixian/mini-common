using Mini.Common.Models;
using Mini.Wms.Abstraction.Models;

namespace Mini.Wms.Abstraction.Services;

public interface IObjectService<TKey, TObj>
{
    Task AddAsync(TObj user, CancellationToken cancellationToken = default);

    Task<IEnumerable<TObj>> AllAsync(CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);

    Task<TObj> GetAsync(TKey id, CancellationToken cancellationToken = default);

    Task<TObj> UpdateAsync(TObj user, CancellationToken cancellationToken = default);

}

public interface IPagedService<TObj>
{
    Task<PagedData<TObj>> PageAsync(PagedDataOptions pagedDataOptions, CancellationToken cancellationToken = default);
}

public interface IUserService<TKey, TObj> :
    IObjectService<TKey, TObj>,
    IPagedService<TObj>
    where TObj : IUser<TKey>
{
}
