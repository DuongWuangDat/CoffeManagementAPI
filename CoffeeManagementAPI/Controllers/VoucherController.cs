﻿using CoffeeManagementAPI.DTOs.Voucher;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.VoucherMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

            var voucherList = await _voucherRepository.GetAllVoucher();

            return Ok(voucherList);
        }

        [HttpGet("getbyid/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var voucher = await _voucherRepository.GetVoucherById(id);

            if(voucher == null)
            {
                return NotFound("Voucher is not found");
            }

            return Ok(voucher);
        }

        [HttpGet("getbycode")]
        public async Task<IActionResult> GetByCode([FromQuery] string code)
        {

            var voucher = await _voucherRepository.GetVoucherByCode(code);
            if(voucher == null)
            {
                return NotFound("Voucher is not found");
            }
            return Ok(voucher);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOne([FromBody] CreatedVoucherDTO createdVoucher)
        {

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

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteVoucher([FromRoute] int id)
        {

            var isSucess = await _voucherRepository.DeleteVoucher(id);

            if (!isSucess)
            {
                return NotFound("Voucher is not found");
            }
            return Ok("Deleted voucher successfully");
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateVoucher([FromRoute] int id, [FromBody] UpdatedVoucherDTO updatedVoucherDTO)
        {

            var (isSuccess, newVoucher) = await _voucherRepository.UpdateVoucher(updatedVoucherDTO.toVoucherFromUpdated(), id);

            if (!isSuccess)
            {
                return BadRequest("Something went wrong");
            }

            return Ok(new
            {
                data = newVoucher,
                message ="Updated successfully"
            });


        }

    }
}