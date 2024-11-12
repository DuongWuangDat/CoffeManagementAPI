using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Product
{
    public class CreateCategoryDTO
    {
        [Required]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        public string Image { get; set; }
    }
}
