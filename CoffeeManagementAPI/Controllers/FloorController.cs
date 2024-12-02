using CoffeeManagementAPI.DTOs.Tables;
using CoffeeManagementAPI.ErrorHandler;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Tble;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/floor")]
    [Authorize]
    public class FloorController : ControllerBase
    {
        private readonly IFloorRepository _floorRepository;
        public FloorController(IFloorRepository floorRepository)
        {
            _floorRepository = floorRepository;
            
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll() {
            var floorList = await _floorRepository.GetAllFloor();

            return Ok(floorList);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateFloorDTO createFloorDTO)
        {
            var floor = createFloorDTO.toFloorFromCreated();
            var (isSuccess, errMsg) = await _floorRepository.CreateNewFloor(floor);
            if (!isSuccess) {
                return BadRequest(new ApiError(errMsg));
            }

            return Ok(new
            {
                message = "Created successfully",
                data = floor
            });
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var (isSuccess, errMsg) = await _floorRepository.DeleteFloor(id);
            if (!isSuccess)
            {
                return BadRequest(new ApiError(errMsg));
            }
            return Ok(new
            {
                message = "Deleted successfully"
            });
        }

    }
}
