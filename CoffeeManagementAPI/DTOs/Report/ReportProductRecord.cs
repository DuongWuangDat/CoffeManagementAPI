namespace CoffeeManagementAPI.DTOs.Report
{
    public class ReportProductRecord
    {
        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty ;
        public int OrderCount { get; set; }
    }
}
