namespace CoffeeManagementAPI.DTOs.Report
{
    public class ReportBill
    {

        public int DoneCount { get; set; }
        public int PendingCount { get; set; }

        public int TotalCount { get; set; }

        public decimal DonePercent { get; set; }

        public decimal PendingPercent { get; set; }

    }
}
