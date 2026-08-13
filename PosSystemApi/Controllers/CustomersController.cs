using Microsoft.AspNetCore.Mvc;
using PosSystemApi.Models;
using PosSystemApi.Services;

namespace PosSystemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers =
                await _customerService.GetAllCustomersAsync();

            return Ok(customers);
        }

        // GET: api/customers/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            Customer? customer =
                await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            return Ok(customer);
        }

        // POST: api/customers
        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            try
            {
                Customer createdCustomer =
                    await _customerService.AddCustomerAsync(customer);

                return Ok(createdCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/customers/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            bool deleted =
                await _customerService.DeleteCustomerAsync(id);

            if (!deleted)
            {
                return NotFound("Customer not found.");
            }

            return Ok("Customer deleted successfully.");
        }
    }
}