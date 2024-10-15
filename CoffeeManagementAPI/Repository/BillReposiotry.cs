using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.BillMapper;
using CoffeeManagementAPI.Model;
using CoffeeManagementAPI.QueryObject;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class BillRepository : IBillRepository
    {
        ApplicaitonDBContext _context;
        public BillRepository(ApplicaitonDBContext context)
        {
             _context = context;
        }
        public async Task<bool> CreateNewBill(Bill bill)
        {
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.VoucherID == bill.VoucherId);

            if (voucher == null)
            {
                return false;
            }

            bill.VoucherValue = voucher.VoucherValue;
            bill.VoucherTypeIndex = (int)voucher.VoucherTypeId;

            var cusID = bill.CustomerId;

            if(cusID != null)
            {
                var cus = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == cusID);

                if(cus == null)
                {
                    return false;
                }

                cus.Revenue += bill.TotalPrice;

                var customerType = await _context.CustomerTypes.OrderByDescending(c => c.BoundaryRevenue).FirstOrDefaultAsync(c => c.BoundaryRevenue <= cus.Revenue);

                if(customerType != null)
                {
                    cus.CustomerTypeId = customerType.CustomerTypeID;
                }
            }

            await _context.AddAsync(bill);
            await _context.SaveChangesAsync();

            return true;

        }

        public Task DeleteBill(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BillFromGetAllDTO>> GetAllBill()
        {
            var billList = await _context.Bills.Select(b => b.toBillFromGetAllDTO()).ToListAsync();

            return billList;
        }

        public async Task<BillDTO?> GetBillById(int id)
        {
            var bill = await _context.Bills.Include(b=> b.BillDetails).FirstOrDefaultAsync(b => b.BillId == id);
            if(bill == null)
            {
                return null;
            }

            return bill.toBillDTO();
        }

        public async Task<List<BillFromGetAllDTO>> GetBillPagination(PaginationObject pagination)
        {
            var billSelectable = _context.Bills.Select(b => b.toBillFromGetAllDTO()).AsQueryable();
            var billList = await billSelectable.Skip(pagination.pageSize* (pagination.page-1)).Take(pagination.pageSize).ToListAsync();

            return billList;
        }
    }
}
