namespace CoffeeManagementAPI.DTOs.Report
{
    public class ReportRevenue
    {

        public decimal TotalValue { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<ReportRecordRevenue> reportRecordRevenues { get; set; } = new List<ReportRecordRevenue>();

    }
}
