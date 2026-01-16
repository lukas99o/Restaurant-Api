using ResturangDB_API.Models;
using ResturangDB_API.Models.DTOs.Customer;

namespace ResturangDB_API.Services.IServices
{
    public interface ICustomerService
    {
        Task AddCustomerAsync(CustomerCreateDTO customer);
        Task<IEnumerable<CustomerGetDTO>> GetAllCustomersAsync();
        Task<CustomerGetDTO> GetCustomerByIdAsync(int customerID);
        Task<bool> UpdateCustomerAsync(CustomerUpdateDTO customer);
        Task<bool> DeleteCustomerAsync(int customerID);
    }
}
