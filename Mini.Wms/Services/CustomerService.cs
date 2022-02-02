namespace Mini.Wms.Services;

public interface ICustomerService
{
    IList<ICustomer> GetCustomerList();

    void Add(ICustomer customer);

    void Update(ICustomer customer);

    void Archive(ICustomer customer);

    void Remove(ICustomer customer);

}

public class CustomerService : ICustomerService
{
    public void Add(ICustomer customer)
    {
        throw new NotImplementedException();
    }

    public void Archive(ICustomer customer)
    {
        throw new NotImplementedException();
    }

    public IList<ICustomer> GetCustomerList()
    {
        throw new NotImplementedException();
    }

    public void Remove(ICustomer customer)
    {
        throw new NotImplementedException();
    }

    public void Update(ICustomer customer)
    {
        throw new NotImplementedException();
    }
}
