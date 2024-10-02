using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.Model
{
    public class BillDetail
    {
        public int BillId { get; set;}

        public Bill? Bill { get; set; }

        
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int ProductCount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPriceDtail { get; set; }
    }
}
