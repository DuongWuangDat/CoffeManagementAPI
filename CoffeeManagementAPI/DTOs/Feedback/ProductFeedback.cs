using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Feedback
{
    public class ProductFeedback
    {

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Star { get; set; }

    }
}
