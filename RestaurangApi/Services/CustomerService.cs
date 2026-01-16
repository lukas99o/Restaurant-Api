using ResturangDB_API.Data.Repos;
using ResturangDB_API.Data.Repos.IRepos;
using ResturangDB_API.Models;
using ResturangDB_API.Models.DTOs.Customer;
using ResturangDB_API.Services.IServices;

namespace ResturangDB_API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;   
        }

        public async Task AddCustomerAsync(CustomerCreateDTO customer)
        {
            var newCustomer = new Customer
            {
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };

            await _customerRepo.AddCustomerAsync(newCustomer);
        }

        public async Task<IEnumerable<CustomerGetDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepo.GetAllCustomersAsync();

            var customerList = customers.Select(customer => new CustomerGetDTO
            {
                CustomerID = customer.CustomerID,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Password = customer.Password
            }).ToList();

            return customerList;
        }

        public async Task<CustomerGetDTO> GetCustomerByIdAsync(int customerID)
        {
            var customerFound = await _customerRepo.GetCustomerByIDAsync(customerID);
            
            if (customerFound != null)
            {
                var customer = new CustomerGetDTO
                {
                    CustomerID = customerFound.CustomerID,
                    Name = customerFound.Name,
                    Email = customerFound.Email,
                    PhoneNumber = customerFound.PhoneNumber
                };

                return customer;
            }

            return null;
        }

        public async Task<bool> UpdateCustomerAsync(CustomerUpdateDTO customer)
        {
            if (string.IsNullOrEmpty(customer.Name))
            {
                return false;
            }

            var updatedCustomer = await _customerRepo.GetCustomerByIDAsync(customer.CustomerID);
            
            if (updatedCustomer != null)
            {
                updatedCustomer.Name = customer.Name;
                updatedCustomer.Email = customer.Email;
                updatedCustomer.PhoneNumber = customer.PhoneNumber;
                await _customerRepo.UpdateCustomerAsync(updatedCustomer);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteCustomerAsync(int customerID)
        {
            Console.WriteLine(customerID);
            var foundCustomer = await _customerRepo.GetCustomerByIDAsync(customerID);

            if (foundCustomer != null)
            {
                await _customerRepo.DeleteCustomerAsync(foundCustomer);
                return true;
            }

            return false;
        }
    }
}
