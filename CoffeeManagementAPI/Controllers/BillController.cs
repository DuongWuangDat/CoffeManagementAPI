using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.BillMapper;
using CoffeeManagementAPI.QueryObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

            var billList = await _billRepository.GetAllBill();

            return Ok(billList);
        }

        [HttpGet("getbyid/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            var bill = await _billRepository.GetBillById(id);

            return Ok(bill);
        }

        [HttpPost("create")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([FromBody] CreatedBillDTO createdBillDTO)
        {
            
            //var newBill = JsonSerializer.Deserialize<CreatedBillDTO>(json);
            if(createdBillDTO.Status != "Pending" && createdBillDTO.Status != "Successful") {
                return BadRequest(new
                {
                    message = "Bill status should be 'Pending' or 'Successful'"
                });
            }
            var bill = createdBillDTO.toBillFromUpdated();
            var isSuccess = await _billRepository.CreateNewBill(bill);
            if (!isSuccess)
            {
                return BadRequest("Something went wrong");
            }

            return Ok(new
            {
                data = bill.toBillDTO(),
                message = "Created successfully"
            });
        }

        [HttpGet("paginate")]
        public async Task<IActionResult> GetBillPaginate([FromQuery] PaginationObject pagination)
        {
            var billList = await _billRepository.GetBillPagination(pagination);

            return Ok(new
            {
                page= pagination.page,
                pageSize = pagination.pageSize,
                data = billList
            });
        }

    }
}
