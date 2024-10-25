using CoffeeManagementAPI.DTOs.Product;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface ICategoryRepository
    {

        Task<List<CategoryDTO>> GetCategories();

        Task<bool> CreateNewCategory(Category category);

        Task<(bool, string)> DeleteCategory(int id);

    }
}
