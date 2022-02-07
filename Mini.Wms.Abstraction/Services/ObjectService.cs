using Mini.Wms.Abstraction.Models;

namespace Mini.Wms.Abstraction.Services;

public interface IObjectService<TKey, TObj>
{
    Task AddAsync(TObj user, CancellationToken cancellationToken = default);

    void UpdateAsync(T user);

    void Delete(T user);

    T Get(T id);

    IEnumerable<T> All();
}

public interface IUserService<T> : IObjectService<T, IUser<T>>
{
}

public class UserService<T> : IUserService<T>
{
    public void Add(IUser<T> user)
    {
        throw new NotImplementedException();
    }

    public void Delete(IUser<T> user)
    {
        throw new NotImplementedException();
    }

    public IUser<T> Get(IUser<T> id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IUser<T>> All()
    {
        throw new NotImplementedException();
    }

    public void Update(IUser<T> user)
    {
        throw new NotImplementedException();
    }
}