using Microsoft.AspNetCore.Mvc;
using ResturangDB_API.Models;
using ResturangDB_API.Models.DTOs.Customer;
using ResturangDB_API.Services;
using ResturangDB_API.Services.IServices;

namespace ResturangDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CustomerCreateDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _customerService.AddCustomerAsync(customer);
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerGetDTO>>> GetAllCustomers()
        {
            var customerList = await _customerService.GetAllCustomersAsync();
            return Ok(customerList);
        } 

        [HttpGet]
        [Route("GetSpecificCustomer/{customerID}")]
        public async Task<ActionResult<CustomerGetDTO>> GetSpecificCustomer(int customerID)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerID);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut]
        [Route("UpdateCustomer/{customerID}")]
        public async Task<IActionResult> UpdateSpecificCustomer(int customerID, CustomerUpdateDTO customer)
        {
            if (customerID != customer.CustomerID)
            {
                return BadRequest();
            }

            var result = await _customerService.UpdateCustomerAsync(customer);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteCustomer/{customerID}")]
        public async Task<IActionResult> DeleteSpecificCustomer(int customerID)
        {
            var result = await _customerService.DeleteCustomerAsync(customerID);
            
            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
