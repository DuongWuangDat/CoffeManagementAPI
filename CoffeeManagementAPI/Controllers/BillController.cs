using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.ErrorHandler;
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

            if(bill == null)
            {
                return NotFound(new ApiError("Bill is not found"));
            }

            return Ok(bill);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreatedBillDTO createdBillDTO)
        {
            
            //var newBill = JsonSerializer.Deserialize<CreatedBillDTO>(json);
            if(createdBillDTO.Status != "Pending" && createdBillDTO.Status != "Successful") {
                return BadRequest(new ApiError ("Bill status should be 'Pending' or 'Successful'"));
            }
            var bill = createdBillDTO.toBillFromUpdated();
            var (isSuccess, errMsg) = await _billRepository.CreateNewBill(bill);
            if (!isSuccess)
            {
                return BadRequest(new ApiError(errMsg));
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

        [HttpPut("updatestatus/{id:int}")]
        public async Task<IActionResult> UpdateBill([FromBody] BillUpdateStatus billUpdateStatus, [FromRoute] int id)
        {
            var status = billUpdateStatus.Status;
            if(status != "Pending" && status != "Successful")
            {
                return BadRequest(new ApiError("Status must be 'Pending' or 'Successful'"));
            }

            var (isSuccess, errMsg) = await _billRepository.UpdateStatus(id, status);
            if (!isSuccess)
            {
                return BadRequest(new ApiError(errMsg));
            }

            return Ok(new
            {
                message = "Update status successfully"
            });

        }

    }
}
