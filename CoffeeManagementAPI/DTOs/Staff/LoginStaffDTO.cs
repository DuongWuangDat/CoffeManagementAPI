using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Staff
{
    public class LoginStaffDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
