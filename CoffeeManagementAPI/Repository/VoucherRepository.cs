using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Voucher;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.VoucherMapper;
using CoffeeManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        ApplicaitonDBContext _context;
        public VoucherRepository(ApplicaitonDBContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateNewVoucher(Voucher newVoucher)
        {
            await _context.Vouchers.AddAsync(newVoucher);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteVoucher(int id)
        {
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.VoucherID == id);
            if (voucher == null)
            {
                return false;
            }

            _context.Vouchers.Remove(voucher);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<VoucherDTO>> GetAllVoucher()
        {
            var voucherList = await _context.Vouchers.Select(s => s.toVoucherDTO()).ToListAsync();

            return voucherList;
        }

        public async Task<VoucherDTO?> GetVoucherById(int id)
        {
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.VoucherID == id);

            if(voucher == null)
            {
                return null;
            }

            return voucher.toVoucherDTO();
        }

        public Task<bool> UpdateVoucher(Voucher voucher, int id)
        {
            throw new NotImplementedException();
        }
    }
}
