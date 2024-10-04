using CoffeeManagementAPI.DTOs.Product;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface IProductRepository
    {

        Task CreateNewProduct(Product newProduct);

        Task UpdateProduct(Product newProduct, int id);

        Task DeleteProduct(int id);

        Task<List<ProductDTO>> GetAllProduct();

        Task<ProductDTO> GetProductById(int id);

    }
}
