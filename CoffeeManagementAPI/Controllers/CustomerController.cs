using CoffeeManagementAPI.DTOs.Customer;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Cus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/customer")]
    [Authorize(Roles ="Admin")]
    public class CustomerController : ControllerBase
    {
        ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cusList = await _customerRepository.GetAllCustomer();
            return Ok(cusList);
        }

        [HttpGet("getbyid/{id:int}")]
        public async Task<IActionResult> GetById( [FromRoute] int id)
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var cus = await _customerRepository.GetCustomerById(id);
            if (cus == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(cus);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDTO createCustomerDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var newCus = createCustomerDTO.toCustomerFromCreated();

            var isSuccess = await _customerRepository.CreateNewCustomer(newCus);
            if (!isSuccess)
            {
                return BadRequest("Something went wrong");
            }

            return Ok(new
            {
                data = newCus,
                message= "Created successfully"
            }) ;
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteById ([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isSuccess = await _customerRepository.DeleteCustomer(id);

            if (!isSuccess)
            {
                return NotFound("Customer is not found");
            }

            return Ok(new
            {
                message = "Deleted successfully"
            });
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateById([FromRoute] int id, [FromBody] UpdateCustomerDTO updateCustomerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (isSuccess, newCus) = await _customerRepository.UpadateCustomer(updateCustomerDTO.toCustomerFromUpdated(), id);

            if (!isSuccess)
            {
                return NotFound("Customer is not found");
            }

            return Ok(new
            {
                data = newCus,
                message = "Updated successfully"
            });
        }

    }
}
