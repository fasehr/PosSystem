using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();

        Task<Customer?> GetCustomerByIdAsync(int id);

        Task<Customer> AddCustomerAsync(Customer customer);

        Task<bool> DeleteCustomerAsync(int id);
    }
}