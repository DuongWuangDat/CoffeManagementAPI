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
            var voucherList = await _context.Vouchers.Include(v => v.VoucherType).Select(s => s.toVoucherDTO()).ToListAsync();

            return voucherList;
        }

        public async Task<VoucherDTO?> GetVoucherByCode(string code)
        {
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(v=> v.VoucherCode == code);

            if (voucher == null)
            {
                return null;
            }
            return voucher.toVoucherDTO();
        }

        public async Task<VoucherDTO?> GetVoucherById(int id)
        {
            var voucher = await _context.Vouchers.Include(v => v.VoucherType).FirstOrDefaultAsync(v => v.VoucherID == id);

            if(voucher == null)
            {
                return null;
            }

            return voucher.toVoucherDTO();
        }

        public async Task<(bool,Voucher?)> UpdateVoucher(Voucher voucher, int id)
        {
            var vouch = await _context.Vouchers.FirstOrDefaultAsync(p=> p.VoucherID ==  id);
            if (vouch == null)
            {
                return (false, null);
            }
            if(vouch.CreatedDate > voucher.ExpiredDate)
            {
                return (false, null);
            }

            vouch.ExpiredDate = voucher.ExpiredDate;
            vouch.VoucherValue = voucher.VoucherValue;
            vouch.VoucherCode = voucher.VoucherCode;
            vouch.VoucherTypeId = voucher.VoucherTypeId;
            vouch.MaxApply = voucher.MaxApply;

            await _context.SaveChangesAsync();

            return (true, vouch);
           
            
        }
    }
}
