using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.DTOs.Customer
{
    public class CreateCustomerDTO
    {
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        [Required]
        [MinLength(8, ErrorMessage ="PhoneNumber must be 8 characters")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public decimal Revenue { get; set; } = 0;


    }
}
