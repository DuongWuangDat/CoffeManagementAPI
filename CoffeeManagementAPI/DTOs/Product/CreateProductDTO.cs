using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.DTOs.Product
{
    public class CreateProductDTO
    {
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; } = 0;

        public bool IsSoldOut { get; set; } = false;
        [Required]
        public int CategoryId { get; set; }
    }
}
