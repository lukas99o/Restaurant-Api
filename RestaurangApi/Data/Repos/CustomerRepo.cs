using Microsoft.EntityFrameworkCore;
using ResturangDB_API.Data.Repos.IRepos;
using ResturangDB_API.Models;

namespace ResturangDB_API.Data.Repos
{
    public class CustomerRepo : ICustomerRepo                  
    {
        private readonly ResturangContext _context;

        public CustomerRepo(ResturangContext context)
        {
            _context = context;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customerList = await _context.Customers.ToListAsync();
            return customerList;
        }

        public async Task<Customer> GetCustomerByIDAsync(int customerID)
        {
            var customer = await _context.Customers.FindAsync(customerID);
            return customer;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
