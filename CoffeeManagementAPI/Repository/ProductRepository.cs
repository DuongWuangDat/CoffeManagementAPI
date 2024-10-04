using CoffeeManagementAPI.DTOs.Product;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Task CreateNewProduct(Product newProduct)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> GetAllProduct()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(Product newProduct, int id)
        {
            throw new NotImplementedException();
        }
    }
}
