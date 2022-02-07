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

public interface IUserService<T> : IObjectService<T, IUser<T>> {}
