using CoffeeManagementAPI.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/tableType")]
    [Authorize]
    public class TableTypeController : ControllerBase
    {
        private readonly ITableTypeRepository _tableTypeRepo;

        public TableTypeController(ITableTypeRepository tableTypeRepository)
        {
            _tableTypeRepo = tableTypeRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var tableType = await _tableTypeRepo.GetAll();
            return Ok(tableType);
        }
    }
}
