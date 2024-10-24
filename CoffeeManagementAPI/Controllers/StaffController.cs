using CoffeeManagementAPI.DTOs.Staff;
using CoffeeManagementAPI.ErrorHandler;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Sta;
using CoffeeManagementAPI.QueryObject;
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

            var staffs = await _staffRepository.GetAllStaff();

            return Ok(staffs);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {

            var isSuccess = await _staffRepository.DeleteStaff(id);
            if(!isSuccess)
            {
                return NotFound(new ApiError("Staff is not found"));
            }

            return Ok("Deleted successfully");
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> updateStaff([FromBody] UpdatedStaffDTO updatedStaffDTO,[FromRoute] int id)
        {

            var newStaff = updatedStaffDTO.toStaffFromUpdate();

            var (isSuccess,staff) = await _staffRepository.UpdateStaff(newStaff, id);

            if (!isSuccess)
            {
                return NotFound(new ApiError("Staff is not found"));
            }

            return Ok(new
            {
                data= staff,
                message= "Updated successfully"
            });


        }

        [HttpGet("paginate")]
        public async Task<IActionResult> Paginate([FromQuery] PaginationObject pagination)
        {
          

            var staffList = await _staffRepository.GetStaffPagination(pagination);

            return Ok(new
            {
                page = pagination.page,
                pageSize = pagination.pageSize,
                data = staffList
            });

        }


    }
}
