using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Customer
{
    public class CreateCustomerTypeDTO
    {
        [Required]
        public string CustomerTypeName { get; set; } = string.Empty;
        [Required]
        public decimal DiscountValue { get; set; }
        [Required]
        public decimal BoundaryRevenue { get; set; } = 0;
    }
}
