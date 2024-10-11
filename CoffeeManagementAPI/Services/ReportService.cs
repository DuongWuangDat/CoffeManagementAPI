using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Report;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoffeeManagementAPI.Services
{
    public class ReportService : IReportService
    {
        ApplicaitonDBContext _context;
        public ReportService(ApplicaitonDBContext context)
        {
            _context = context;
        }

        public async Task<List<ReportProductRecord>> GetProductRevenue(DateTime start, DateTime end)
        {
            var productQuery = await _context.BillDetails
                .Include(x => x.Bill)
                .Where(p => p.ProductId != null)
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .Where(x => x.Bill.DateTime >= start && x.Bill.DateTime <= end)
                .GroupBy(b => b.ProductId)
                .Select(s => new ReportProductRecord
                {
                    CategoryName = s.First().Product.Category.CategoryName,
                    OrderCount = s.Sum(o => o.ProductCount),
                    ProductName = s.First().ProductName
                }).ToListAsync();

            return productQuery;
        }

        public async Task<ReportBill> GetReportBill(DateTime start, DateTime end)
        {
            var totalCount = await _context.Bills.Where(b=> b.DateTime >= start && b.DateTime <= end).CountAsync();
            if(totalCount == 0)
            {
                return new()
                {
                    DoneCount = 0,
                    TotalCount = 0,
                    DonePercent = 0,
                    PendingCount = 0,
                    PendingPercent = 0,
                };
            }
            var doneOrder = await _context.Bills.Where(b=> b.DateTime>=start && b.DateTime <= end && b.Status=="Đã hoàn thành").CountAsync();
            var pendingOrder = totalCount - doneOrder;
            var donePercent = ((decimal)doneOrder / totalCount) * 100;
            var pendingPercent = 100 - donePercent;

            return new()
            {
                DoneCount = doneOrder,
                DonePercent = donePercent,
                PendingCount = pendingOrder,
                PendingPercent = pendingPercent,
                TotalCount = totalCount,
            };


        }

        public async Task<ReportRevenue?> GetRevenue(DateTime start, DateTime end)
        {
            if(start >= end)
            {
                return null;
            }
            var totalRevenue = await _context.Bills.Where(b=>b.DateTime >= start && b.DateTime <=end).SumAsync(b=>b.TotalPrice);

            List<ReportRecordRevenue> reportRecordRevenues = new List<ReportRecordRevenue>();

            for(var date = start; date <=end; date = date.AddDays(1))
            {
                var revenueRecord = await GetRevenueByDate(date);

                reportRecordRevenues.Add(revenueRecord);
            }

            return new()
            {
                TotalValue = totalRevenue,
                reportRecordRevenues = reportRecordRevenues,
                EndDate = end,
                StartDate = start
            };
        }

        public async Task<ReportRecordRevenue> GetRevenueByDate(DateTime date)
        {
            var totalRevenue = await _context.Bills.Where(b => b.DateTime == date).SumAsync(s => s.TotalPrice);

            var revenueRecord = new ReportRecordRevenue
            {
                DateTime = date,
                Revenue = totalRevenue,
            };

            return revenueRecord;

        }

        public async Task<int> GetTotalOrder(DateTime start, DateTime end)
        {
            var totalOrder = await _context.BillDetails.Where(b => b.Bill.DateTime >= start && b.Bill.DateTime <= end).SumAsync(b => b.ProductCount);

            return totalOrder;
        }
    }
}
