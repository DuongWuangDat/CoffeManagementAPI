using CoffeeManagementAPI.DTOs.Product;
using CoffeeManagementAPI.ErrorHandler;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Prod;
using CoffeeManagementAPI.Model;
using CoffeeManagementAPI.QueryObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/product")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("getall")]
        [Authorize(Roles ="Admin,Staff")]
        public async Task<IActionResult> GetAllProduct() {


            var prodList = await _productRepository.GetAllProduct();

            return Ok(prodList);

        }

        [HttpGet("getbyid/{id:int}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {

            var prod = await _productRepository.GetProductById(id);

            if (prod == null)
            {
                return NotFound(new ApiError("Product is not found"));
            }

            return Ok(prod);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDTO)
        {
            var newProduct = createProductDTO.toProductFromCreate();
            bool isSuccess = await _productRepository.CreateNewProduct(newProduct);
            if (!isSuccess) { 
                return BadRequest(new ApiError("Something went wrong"));
            }

            return Ok(new
            {
                data= newProduct
            });
        }


        [HttpPut("update/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromBody] CreateProductDTO newProductDTO, [FromRoute] int id)
        {
            var newProduct = newProductDTO.toProductFromCreate();
            var (isSuccess,product) =await _productRepository.UpdateProduct(newProduct, id);

            if (!isSuccess)
            {
                return BadRequest(new ApiError("Product is not found"));
            }
            return Ok(new
            {
                data=product,
                message="Updated successfully"
            });
        }

        [HttpDelete("delete/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {

            var isSuccess = await _productRepository.DeleteProduct(id);
            if(!isSuccess)
            {
                return BadRequest(new ApiError("Product is not found"));
            }
            return Ok(new
            {
                message = "Deleted successfully"
            });

        }

        [HttpGet("getproductbycategory/{categoryId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByCategory([FromRoute] int categoryId)
        {

            var prodList = await _productRepository.GetProductByCategory(categoryId);

            return Ok(prodList);
        }

        [HttpGet("paginate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPaginate([FromQuery] PaginationObject pagination)
        {
            
            var prodList = await _productRepository.GetProductPagination(pagination);

            return Ok(prodList);

        }
     
        [HttpPost("updatesoldout/{id:int}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> UpdateSoldOut([FromBody] UpdateProductSoldOutDTO updateProduct, [FromRoute] int id)
        {
            var (isSuccess, errMsg) = await _productRepository.UpdateProductSoldOut(updateProduct.isSoldOut, id);
            if (!isSuccess)
            {
                return BadRequest(new ApiError($"{errMsg}"));
            }
            return Ok(new
            {
                message = "Update successfully"
            });
        }
    }
}
