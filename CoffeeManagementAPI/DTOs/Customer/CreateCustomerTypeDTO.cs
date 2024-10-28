using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Customer
{
    public class CreateCustomerTypeDTO
    {
        [Required]
        public string CustomerTypeName { get; set; } = string.Empty;
        [Required]
        [Range(0,100, ErrorMessage ="Discount value must be between 0 and 100")]
        public decimal DiscountValue { get; set; }
        [Required]
        public decimal BoundaryRevenue { get; set; } = 0;
    }
}
