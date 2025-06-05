using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.Handler.BillHandler;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.BillMapper;
using CoffeeManagementAPI.Model;
using CoffeeManagementAPI.QueryObject;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class BillRepository : IBillRepository
    {
        ApplicationDBContext _context;
        public BillRepository(ApplicationDBContext context)
        {
             _context = context;
        }
        public async Task<(bool,string)> CreateNewBill(Bill bill)
        {
            var voucherHandler = new VoucherHandler(_context);
            var customerHandler = new CustomerHandler(_context);
            var saveHandler = new SaveBillHandler(_context);

            voucherHandler.SetNext(customerHandler).SetNext(saveHandler);

            return await voucherHandler.HandleAsync(bill);

        }

        public Task DeleteBill(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BillDTO>> GetAllBill()
        {
            var billList = await _context.Bills.Include(b=> b.BillDetails).Include(b => b.Customer).Include(b => b.Staff).Include(b => b.PayType).Select(b => b.toBillDTO()).ToListAsync();
           
            return billList;
        }

        public async Task<BillDTO?> GetBillById(int id)
        {
            var bill = await _context.Bills.Include(b=> b.BillDetails).Include(b => b.Customer).Include(b => b.Staff).Include(b => b.PayType).FirstOrDefaultAsync(b => b.BillId == id);
            if(bill == null)
            {
                return null;
            }

            return bill.toBillDTO();
        }

        public async Task<List<BillDTO>> GetBillPagination(PaginationObject pagination)
        {
            var billSelectable = _context.Bills.Include(b => b.BillDetails).Include(b=> b.Customer).Include(b=> b.Staff).Include(b=>b.PayType).Select(b => b.toBillDTO()).AsQueryable();
            var billList = await billSelectable.Skip(pagination.pageSize* (pagination.page-1)).Take(pagination.pageSize).ToListAsync();

            return billList;
        }

        public async Task<(bool, string)> UpdateStatus(int id, string status)
        {
            var bill = await _context.Bills.FirstOrDefaultAsync(b=> b.BillId == id);
            if(bill == null)
            {
                return (false, "Bill is not found");
            }
            if(bill.Status.Equals(status))
            {
                return (false, "Bill status did not change");
            }
            bill.Status = status;
            await _context.SaveChangesAsync();

            return (true, "Update status bill successfully");
        }
    }
}
