using CoffeeManagementAPI.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/paytype")]
    [Authorize]
    public class PayTypeController : ControllerBase
    {
        IPayTypeRepository _paytypeRepository;

        public PayTypeController(IPayTypeRepository payTypeRepository)
        {
            
            _paytypeRepository = payTypeRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll() { 

            var payTyps = await _paytypeRepository.GetAll();
            return Ok(payTyps);
        }


    }
}
