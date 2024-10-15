using CoffeeManagementAPI.DTOs.Customer;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Cus;
using CoffeeManagementAPI.QueryObject;
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

            var cusList = await _customerRepository.GetAllCustomer();
            return Ok(cusList);
        }

        [HttpGet("getbyid/{id:int}")]
        public async Task<IActionResult> GetById( [FromRoute] int id)
        {

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

        [HttpGet("paginate")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Paginate([FromQuery] PaginationObject pagination)
        {

            var cusList = await _customerRepository.GetCustomerPagination(pagination);

            return Ok(new
            {
                page = pagination.page,
                pageSize = pagination.pageSize,
                data= cusList
            }) ;
        }

        [HttpGet("isuserexist")]
        [Authorize(Roles ="Admin,Staff")]
        public async Task<IActionResult> IsUserExist([FromQuery] string phoneNumber) {
            var cus = await _customerRepository.GetCustomerByPhonenumber(phoneNumber);

            if (cus == null)
            {
                return NotFound(new
                {
                    message = "User is not found"
                }) ;
            }

            return Ok(cus);
        
        }

    }
}
