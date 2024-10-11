using CoffeeManagementAPI.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.DTOs.Bill
{
    public class CreateBillDetailDTO
    {

        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public decimal ProductPrice { get; set; }
        [Required]
        public int ProductCount { get; set; }
        [Required]
        public decimal TotalPriceDtail { get; set; }

    }
}
