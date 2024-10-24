using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public string ProductName { get; set; } =string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0;
        public string Image { get; set; } = string.Empty;
        public bool IsSoldOut { get; set; } = false;
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
