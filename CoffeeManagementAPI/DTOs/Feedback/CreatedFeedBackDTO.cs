using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Feedback
{
    public class CreatedFeedBackDTO
    {
        [Required]
        public int StarNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phonenumber { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
