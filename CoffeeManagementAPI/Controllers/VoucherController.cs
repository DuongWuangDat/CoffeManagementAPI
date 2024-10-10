using CoffeeManagementAPI.DTOs.Voucher;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.VoucherMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/voucher")]
    [Authorize(Roles ="Admin")]
    public class VoucherController : ControllerBase
    {
        IVoucherRepository _voucherRepository;

        public VoucherController(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll() 
        { 
            if(!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }

            var voucherList = await _voucherRepository.GetAllVoucher();

            return Ok(voucherList);
        }

        [HttpGet("getbyid/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var voucher = await _voucherRepository.GetVoucherById(id);

            if(voucher == null)
            {
                return NotFound("Voucher is not found");
            }

            return Ok(voucher);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOne([FromBody] CreatedVoucherDTO createdVoucher)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!createdVoucher.IsValidation())
            {
                return BadRequest("ExpiredDate must be greater than CreatedDate");
            }
            var newVoucher = createdVoucher.toVoucherFromCreated();
            var isSuccess = await _voucherRepository.CreateNewVoucher(newVoucher);

            if (!isSuccess)
            {
                return BadRequest("Something went wrong"); 
            }
            return CreatedAtAction(nameof(GetById), new {id = newVoucher.VoucherID}, newVoucher.toVoucherDTO());

        }

        [HttpDelete("delete/{id:id}")]
        public async Task<IActionResult> DeleteVoucher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isSucess = await _voucherRepository.DeleteVoucher(id);

            if (!isSucess)
            {
                return NotFound("Voucher is not found");
            }
            return Ok("Deleted voucher successfully");
        }

    }
}
