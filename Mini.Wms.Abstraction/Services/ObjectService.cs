using Mini.Wms.Abstraction.Models;

namespace Mini.Wms.Abstraction.Services;

public interface IObjectService<T>
{
    void Add(T user);

    void Update(T user);

    void Delete(T user);

    T Get(T id);

    IEnumerable<T> All();
}

public interface IUserService<T> : IObjectService<IUser<T>>
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