using ResturangDB_API.Models;

namespace ResturangDB_API.Data.Repos.IRepos
{
    public interface ICustomerRepo
    {
        Task AddCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIDAsync(int customerID);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Customer customer);
    }
}
