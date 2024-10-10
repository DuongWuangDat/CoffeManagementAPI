using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeManagementAPI.DTOs.Customer
{
    public class CustomerTypeDTO
    {
        public int CustomerTypeID { get; set; }
        public string CustomerTypeName { get; set; } = string.Empty;

        public decimal DiscountValue { get; set; }
        public decimal BoundaryRevenue { get; set; } = 0;
    }
}
