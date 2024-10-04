using CoffeeManagementAPI.DTOs.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.DTOs.Bill
{
    public class BillDetailDTO
    {


        public int ProductId { get; set; }

        public ProductDTO? Product { get; set; }

        public int ProductCount { get; set; }
        public decimal TotalPriceDtail { get; set; }

    }
}
