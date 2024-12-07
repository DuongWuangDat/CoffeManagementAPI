using CoffeeManagementAPI.DTOs.Tables;
using CoffeeManagementAPI.ErrorHandler;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Tble;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/table")]
    [Authorize]
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _tableRepository;
        public TableController(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll() { 
            var tables = await _tableRepository.GetTables();
            return Ok(tables);
        }

        [HttpGet("getTableByFloor/{floorId:int}")]
        public async Task<IActionResult> GetTableByFloorId([FromRoute] int floorId)
        {
            var tables =await _tableRepository.GetTableByFloorID(floorId);
            return Ok(tables);
        }

        [HttpGet("getTableByType/{typeId:int}")]
        public async Task<IActionResult> GetTableByType([FromRoute] int typeId)
        {
            var tables = await _tableRepository.GetTableByType(typeId);
            return Ok(tables);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTable([FromBody] CreateTableDTO createTableDTO)
        {
            var table = createTableDTO.toTableFromCreate();
            var (isSuccess, errMsg) = await _tableRepository.CreateNewTable(table);
            if(!isSuccess)
            {
                return BadRequest(new ApiError(errMsg));
            }

            return Ok(new
            {
                data = table
            });
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteTable([FromRoute] int id)
        {
            var(isSuccess, errMsg) = await _tableRepository.DeleteTable(id);
            if(!isSuccess)
            {
                return BadRequest(new ApiError(errMsg));
            }

            return Ok(new
            {
                message = "Deleted successfully"
            });
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateTable([FromBody] UpdateTableDTO updateTableDTO, [FromRoute] int id)
        {
            if (!updateTableDTO.isStatucCorrect())
            {
                return BadRequest(new ApiError("Status must be in Not booked, Booked or Under repair"));
            }
            var (isSuccess, msg) = await _tableRepository.UpdateTable(updateTableDTO, id);
            if(!isSuccess)
            {
                return BadRequest(new ApiError($"{msg}"));
            }

            return Ok(new
            {
                message = msg
            });
        }

        [HttpPut("updateStatus/{id:int}")]
        public async Task<IActionResult> UpdateStatusTable([FromRoute] int id, [FromBody] UpdateStatusTableDTO updateStatusTableDTO)
        {
            if (!updateStatusTableDTO.isStatucCorrect())
            {
                return BadRequest(new ApiError("Status must be in Not booked, Booked or Under repair"));
            }
            var (isSuccess, msg) = await _tableRepository.UpdateStatusTable(updateStatusTableDTO, id);
            if (!isSuccess)
            {
                return BadRequest(new ApiError(msg));
            }
            return Ok(new
            {
                message = "Updated successfully"
            });
        }

        [HttpPost("booking")]
        public async Task<IActionResult> BookTable([FromBody] BookingTableDTO bookingTableDTO)
        {
            var (isSuccess ,msg) = await _tableRepository.BookTable(bookingTableDTO);
            if(!isSuccess)
            {
                return BadRequest(new ApiError(msg));
            }
            return Ok(new
            {
                message = "Booking successfully"
            });
        }

        [HttpPost("endingTable/{tableId:int}")]
        public async Task<IActionResult> EndTable([FromRoute] int tableId)
        {
            var (isSuccess, msg) = await _tableRepository.EndTable(tableId);
            if (!isSuccess)
            {
                return BadRequest(new ApiError(msg));
            }
            return Ok(new
            {
                message = "Ending successfully"
            });
        }

        [HttpPost("getBillFromBookingTable/{tableId:int}")]
        public async Task<IActionResult> GetBillFromBookingTable([FromRoute] int tableId)
        {
            var bill = await _tableRepository.GetBillFromBookingTable(tableId);
            return Ok(bill);
        }

    }
}
