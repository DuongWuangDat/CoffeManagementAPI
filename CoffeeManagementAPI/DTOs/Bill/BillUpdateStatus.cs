using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.DTOs.Bill
{
    public class BillUpdateStatus
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
