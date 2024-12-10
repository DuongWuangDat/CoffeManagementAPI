using CoffeeManagementAPI.DTOs.Product;

namespace CoffeeManagementAPI.DTOs.Report
{
    public class ReportProductRecord
    {
        public ProductDTO? Product { get; set; }
        public int OrderCount { get; set; }
    }
}
