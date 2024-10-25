using CoffeeManagementAPI.DTOs.Product;
using CoffeeManagementAPI.ErrorHandler;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Cate;
using CoffeeManagementAPI.QueryObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/category")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prdList = await _categoryRepository.GetCategories();

            return Ok(prdList);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            var cate = createCategoryDTO.toCategoryFromCreate();
            await _categoryRepository.CreateNewCategory(cate);
            return Ok(new
            {
                data = cate,
                message="Created successfully"
            }) ;

        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var (isSuccess, errMsg) = await _categoryRepository.DeleteCategory(id);
            if (!isSuccess) {
                return NotFound(new ApiError(errMsg));
            }

            return Ok(new
            {
                message = "Deleted successfully"
            });
        }


    }
}
