using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Staff
{
    public class RegisterStaffDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Username cannot be over 100 characters")]
        [MinLength(5, ErrorMessage = "Username must be 10 characters")]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MaxLength(20, ErrorMessage = "Password cannot be over 20 characters")]
        [MinLength(5, ErrorMessage = "Password must be 10 characters")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = "StaffName cannot be over 100 characters")]
        [MinLength(5, ErrorMessage = "StaffName must be 10 characters")]
        public string StaffName { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
    }
}
