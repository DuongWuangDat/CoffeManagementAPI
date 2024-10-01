using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.Model
{
    public class CustomerType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerTypeID { get; set; }
        public string CustomerTypeName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal BoundaryRevenue { get; set; } = 0;
    }
}
