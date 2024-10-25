using CoffeeManagementAPI.DTOs.Product;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Mappers.Cate
{
    public static class Cate
    {

        public static CategoryDTO toCategoryDTO(this Category category)
        {
            return new CategoryDTO
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,

            };
        }

        public static Category toCategoryFromCreate(this CreateCategoryDTO createCategoryDTO)
        {
            return new Category
            {
                CategoryName = createCategoryDTO.CategoryName,
                Image = createCategoryDTO.Image,
            };
        }

    }
}
