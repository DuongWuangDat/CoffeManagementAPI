using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Staff
{
    public class UpdatedStaffDTO
    {

        [Required]
        [MaxLength(100, ErrorMessage = "Username cannot be over 100 characters")]
        [MinLength(5, ErrorMessage = "Username must be 10 characters")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(100, ErrorMessage = "StaffName cannot be over 100 characters")]
        [MinLength(5, ErrorMessage = "StaffName must be 10 characters")]
        public string StaffName { get; set; } = string.Empty;
    }
}
