using CoffeeManagementAPI.DTOs.Product;
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
    [Authorize(Roles = "Admin")]
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
                return NotFound("Product is not found");
            }

            return Ok(prod);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDTO)
        {
            var newProduct = createProductDTO.toProductFromCreate();
            bool isSuccess = await _productRepository.CreateNewProduct(newProduct);
            if (!isSuccess) { 
                return BadRequest("Something went wrong");
            }

            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ProductID }, newProduct.toProdDTO());
        }


        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromBody] CreateProductDTO newProductDTO, [FromRoute] int id)
        {
            var newProduct = newProductDTO.toProductFromCreate();
            var (isSuccess,product) =await _productRepository.UpdateProduct(newProduct, id);

            if (!isSuccess)
            {
                return BadRequest("Something went wrong");
            }
            return Ok(new
            {
                data=product,
                message="Updated successfully"
            });
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {

            var isSuccess = await _productRepository.DeleteProduct(id);
            if(!isSuccess)
            {
                return BadRequest("Something went wrong");
            }
            return Ok(new
            {
                message = "Deleted successfully"
            });

        }

        [HttpGet("getproductbycategory/{categoryId:int}")]
        public async Task<IActionResult> GetByCategory([FromRoute] int categoryId)
        {

            var prodList = await _productRepository.GetProductByCategory(categoryId);

            return Ok(prodList);
        }

        [HttpGet("paginate")]
        public async Task<IActionResult> GetPaginate([FromQuery] PaginationObject pagination)
        {
            
            var prodList = await _productRepository.GetProductPagination(pagination);

            return Ok(prodList);

        }

    }
}
