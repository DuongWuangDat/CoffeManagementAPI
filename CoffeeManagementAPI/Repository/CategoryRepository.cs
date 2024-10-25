using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Product;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Cate;
using CoffeeManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        ApplicationDBContext _context;

        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateNewCategory(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<(bool, string)> DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryID == id);
            if (category == null)
            {
                return (false, "Category is not found");

            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return (true, "");
        }

        public async Task<List<CategoryDTO>> GetCategories()
        {
            var cateList = await _context.Categories.Select(s => s.toCategoryDTO()).ToListAsync();

            return cateList;
        }
    }
}
