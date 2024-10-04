using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.DTOs.Product
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;

        public bool IsSoldOut { get; set; } = false;
        public string CategoryName { get; set; } = string.Empty ;
    }
}
