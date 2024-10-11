using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.BillMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/bill")]
    [Authorize]
    public class BillController : ControllerBase
    {

        IBillRepository _billRepository;

        public BillController(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var billList = await _billRepository.GetAllBill();

            return Ok(billList);
        }

        [HttpGet("getbyid/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bill = await _billRepository.GetBillById(id);

            return Ok(bill);
        }

        [HttpPost("create")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([FromBody] CreatedBillDTO createdBillDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bill = createdBillDTO.toBillFromUpdated();
            var isSuccess = await _billRepository.CreateNewBill(bill);
            if (!isSuccess)
            {
                return BadRequest("Something went wrong");
            }

            return Ok(new
            {
                data = bill,
                message = "Created successfully"
            });
        }

    }
}
