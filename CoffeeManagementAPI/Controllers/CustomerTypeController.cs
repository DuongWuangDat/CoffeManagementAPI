using CoffeeManagementAPI.DTOs.Customer;
using CoffeeManagementAPI.ErrorHandler;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Cus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/customertype")]
    [Authorize(Roles ="Admin")]
    public class CustomerTypeController : ControllerBase
    {

        ICustomerTypeRepository _customerTypeRepository;

        public CustomerTypeController(ICustomerTypeRepository customerTypeRepository)
        {
            _customerTypeRepository = customerTypeRepository;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll() { 
            var cusTypes = await _customerTypeRepository.GetAllCustomerType();

            return Ok(cusTypes);
        
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCustomerTypeDTO createCustomerTypeDTO)
        {
            var cusType = createCustomerTypeDTO.toCustomerFromCreate();
            var isSuccess = await _customerTypeRepository.CreateNewCustomerType(cusType);

            if (!isSuccess)
            {
                return BadRequest(new ApiError("Something went wrong"));
            }

            return Ok(new
            {
                data = cusType,
                message = "Created successfully"
            }) ;
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> Update([FromBody] CreateCustomerTypeDTO updateCustomerTypeDTO, [FromRoute] int id)
        {
            var cusType = updateCustomerTypeDTO.toCustomerFromCreate();
            var isSuccess = await _customerTypeRepository.UpdateCustomerType(cusType, id);
            if(!isSuccess)
            {
                return NotFound(new ApiError("Customer is not found"));
            }

            return Ok(new
            {
                message="Update successfully"
            });
        }


        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var isSuccess = await _customerTypeRepository.DeleteCustomerType(id);
            if (!isSuccess)
            {
                return NotFound(new ApiError("Customer type is not found"));
            }

            return Ok(new
            {
                message = "Deleted successfully"
            });
        }
    }
}
