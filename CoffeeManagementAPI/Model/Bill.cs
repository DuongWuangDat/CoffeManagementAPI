using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.Model
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillId { get; set; }

        public string Status { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int VoucherId { get; set; }
        public Voucher? Voucher { get; set; }
        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int PayTypeId { get; set; }
        public PayType? PayType { get; set; }
    }
}
