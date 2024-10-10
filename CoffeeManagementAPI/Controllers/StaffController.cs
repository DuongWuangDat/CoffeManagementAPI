using CoffeeManagementAPI.DTOs.Staff;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Sta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/staff")]
    [Authorize(Roles = "Admin")]
    public class StaffController : ControllerBase
    {
        IStaffRepository _staffRepository;
        public StaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;  
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var staffs = await _staffRepository.GetAllStaff();

            return Ok(staffs);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isSuccess = await _staffRepository.DeleteStaff(id);
            if(!isSuccess)
            {
                return NotFound("Staff is not found");
            }

            return Ok("Deleted successfully");
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> updateStaff([FromBody] UpdatedStaffDTO updatedStaffDTO,[FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newStaff = updatedStaffDTO.toStaffFromUpdate();

            var (isSuccess,staff) = await _staffRepository.UpdateStaff(newStaff, id);

            if (!isSuccess)
            {
                return NotFound("New staff is updated");
            }

            return Ok(new
            {
                data= staff,
                message= "Updated successfully"
            });


        }

    }
}
