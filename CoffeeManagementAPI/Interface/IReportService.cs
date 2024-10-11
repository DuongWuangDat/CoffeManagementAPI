using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.DTOs.Report;

namespace CoffeeManagementAPI.Interface
{
    public interface IReportService
    {
        //Revenue
        Task<ReportRecordRevenue?> GetRevenueByDate(DateTime date);

        Task<ReportRevenue?> GetRevenue(DateTime start, DateTime end);
        //Product
        Task<List<ReportProductRecord>> GetProductRevenue(DateTime start, DateTime end);
        Task<int> GetTotalOrder(DateTime start, DateTime end);

        //Bill
        Task<ReportBill> GetReportBill (DateTime start, DateTime end);

    }
}
