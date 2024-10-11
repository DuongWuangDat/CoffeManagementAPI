using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.SendEmail
{
    public class SendEmailDTO
    {
        [Required]
        public string email { get; set; } = string.Empty;

        [Required]
        public string code { get; set; } = string.Empty;
    }
}
