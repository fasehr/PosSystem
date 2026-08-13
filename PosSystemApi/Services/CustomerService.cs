using Microsoft.EntityFrameworkCore;
using PosSystemApi.Data;
using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);

            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            Customer? customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}