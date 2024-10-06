using CoffeeManagementAPI.DTOs.Product;

namespace CoffeeManagementAPI.Interface
{
    public interface ICategoryRepository
    {

        Task<List<CategoryDTO>> GetCategories();

    }
}
