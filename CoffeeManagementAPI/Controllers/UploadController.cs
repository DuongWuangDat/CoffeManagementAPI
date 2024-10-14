using CoffeeManagementAPI.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/upload")]
    [Authorize]
    public class UploadController : ControllerBase
    {

        ICloudinaryService _cloudinaryService;
        public UploadController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var imageURL = await _cloudinaryService.UploadImage(file);

            if (imageURL == null)
            {
                return BadRequest("Something went wrong");
            }

            return Ok(new
            {
                imageUrl = imageURL,
            });
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteImage([FromQuery] string url)
        {
            var isSuccess= await _cloudinaryService.DeleteImage(url);
            if (!isSuccess)
            {
                return BadRequest("Something went wrong");
            }

            return Ok(new
            {
                message = "Deleted Successfully"
            });
        }

    }
}
