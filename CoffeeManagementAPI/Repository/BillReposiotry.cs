using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.BillMapper;
using CoffeeManagementAPI.Model;
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
        public Task CreateNewBill(Bill bill)
        {
            throw new NotImplementedException();
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
    }
}
