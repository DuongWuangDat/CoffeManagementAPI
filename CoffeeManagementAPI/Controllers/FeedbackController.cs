using CoffeeManagementAPI.DTOs.Feedback;
using CoffeeManagementAPI.ErrorHandler;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Fb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/feedback")]
    public class FeedbackController : ControllerBase
    {
        public readonly IFeedBackRepository _feedbackRepository;
        public FeedbackController(IFeedBackRepository feedBackRepository)
        {
            _feedbackRepository = feedBackRepository;
        }

        
        [HttpGet("getall")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var fbList = await _feedbackRepository.GetAllFeedBack();
            return Ok(fbList);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreatedFeedBackDTO createdFeedBackDTO)
        {
            var newFb = createdFeedBackDTO.toFeedbackFromCreated();
            var (isSuccess, errMsg ) = await _feedbackRepository.CreateFeedBack(newFb);
            if (!isSuccess)
            {
                return BadRequest(new ApiError(errMsg));
            }

            return Ok(new
            {
                message = "Created successfully",
                data = newFb.toFeedBackDTO()
            }) ;
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var (isSuccess, errMsg) = await _feedbackRepository.DeleteFeedBack(id);
            if (!isSuccess)
            {
                return BadRequest(new ApiError(errMsg));
            }

            return Ok(new
            {
                message = "Deleted sucessfully"
            });
        }

    }
}
