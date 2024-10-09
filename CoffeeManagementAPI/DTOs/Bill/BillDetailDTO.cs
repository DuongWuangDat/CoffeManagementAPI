using CoffeeManagementAPI.DTOs.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.DTOs.Bill
{
    public class BillDetailDTO
    {


        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }

        public int ProductCount { get; set; }
        public decimal TotalPriceDtail { get; set; }

    }
}
