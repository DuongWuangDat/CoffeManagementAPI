
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.Model
{
    public class BillDetail 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillDetailId {  get; set; }

        public int BillId { get; set;}

        public Bill? Bill { get; set; }

        
        public int? ProductId { get; set; }

        public Product? Product { get; set; }

        public string ProductName { get; set; } = string.Empty;
        [Column(TypeName ="decimal(18,2)")]
        public decimal ProductPrice { get; set; } 

        public int ProductCount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPriceDtail { get; set; }
    }
}
