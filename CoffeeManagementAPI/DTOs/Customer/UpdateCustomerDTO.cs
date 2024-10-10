using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Customer
{
    public class UpdateCustomerDTO
    {

        [Required]
        public string CustomerName { get; set; } = string.Empty;
        [Required]
        [MinLength(8, ErrorMessage = "PhoneNumber must be 8 characters")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
