using Mini.Wms.DataAbstraction.Models;

namespace Mini.Wms.Abstraction.Services;

public interface IUserService<T>
{
    void Add(IUser<T> user);

    void Update(IUser<T> user);

    void Delete(IUser<T> user);

    void Get(T id);
}
