using Mini.Wms.Abstraction.Models;

namespace Mini.Wms.Abstraction.Services;

public interface IObjectService<T>
{
    void AddAsync(T user, CancellationToken cancellationToken = default);

    void UpdateAsync(T user);

    void Delete(T user);

    T Get(T id);

    IEnumerable<T> All();
}

public interface IUserService<T> : IObjectService<IUser<T>>
{
}
