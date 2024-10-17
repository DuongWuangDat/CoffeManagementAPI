using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Product;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Cate;
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
        public async Task<List<CategoryDTO>> GetCategories()
        {
            var cateList = await _context.Categories.Select(s => s.toCategoryDTO()).ToListAsync();

            return cateList;
        }
    }
}
