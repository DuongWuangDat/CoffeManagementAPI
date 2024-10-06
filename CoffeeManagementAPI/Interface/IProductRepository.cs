using CoffeeManagementAPI.DTOs.Product;
using CoffeeManagementAPI.Model;
using CoffeeManagementAPI.QueryObject;

namespace CoffeeManagementAPI.Interface
{
    public interface IProductRepository
    {

        Task<bool> CreateNewProduct(Product newProduct);

        Task<bool> UpdateProduct(Product newProduct, int id);

        Task<bool> DeleteProduct(int id);

        Task<List<ProductDTO>> GetAllProduct();

        Task<ProductDTO?> GetProductById(int id);

        Task<List<ProductDTO>> GetProductByCategory(int categoryId);

        Task<List<ProductDTO>> GetProductPagination(PaginationObject pagination);

    }
}
