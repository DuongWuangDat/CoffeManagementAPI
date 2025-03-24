using CoffeeManagementAPI.ErrorHandler;
using CoffeeManagementAPI.Factory;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Interface.StrategyInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/upload")]
    [Authorize]
    public class UploadController : ControllerBase
    {

        StorageFactory _storageFactory;
        IStorageStrategy _storageStrategy;
        public UploadController(StorageFactory storageFactory)
        {
            _storageFactory = storageFactory;
            _storageStrategy = _storageFactory.GetStorageStragery("CLOUDINARY");

        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            
            if (file == null || file.Length == 0)
            {
                return BadRequest(new ApiError("No file uploaded."));
            }

            var imageURL = await _storageStrategy.UploadImage(file);

            if (imageURL == null)
            {
                return BadRequest(new ApiError("Something went wrong"));
            }

            return Ok(new
            {
                imageUrl = imageURL,
            });
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteImage([FromQuery] string url)
        {
            var isSuccess= await _storageStrategy.DeleteImage(url);
            if (!isSuccess)
            {
                return BadRequest(new ApiError("Something went wrong"));
            }

            return Ok(new
            {
                message = "Deleted Successfully"
            });
        }

    }
}
