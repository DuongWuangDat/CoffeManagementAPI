using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Voucher;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.VoucherMapper;
using CoffeeManagementAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoffeeManagementAPI.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        ApplicationDBContext _context;
        public VoucherRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateNewVoucher(Voucher newVoucher)
        {
            await _context.Vouchers.AddAsync(newVoucher);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<(bool, string)> DeleteManyVoucher(IEnumerable<int> setOfVoucherId)
        {
            if (setOfVoucherId == null)
            {
                return (false, "setOfVoucherId is not null");
            }
            
            var voucherList = await _context.Vouchers.Where(v=> setOfVoucherId.Contains(v.VoucherID)).ToListAsync();
            foreach(Voucher voucher in voucherList)
            {
                _context.Remove(voucher);
            }
            await _context.SaveChangesAsync();
            return (true, "");
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
            var voucher = await _context.Vouchers.Include(v=>v.VoucherType).FirstOrDefaultAsync(v=> v.VoucherCode == code && v.ExpiredDate >= DateTime.Now && v.MaxApply>0);

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

        public async Task<(bool,Voucher?, string)> UpdateVoucher(Voucher voucher, int id)
        {
            var vouch = await _context.Vouchers.FirstOrDefaultAsync(p=> p.VoucherID ==  id);
            if (vouch == null)
            {
                return (false, null, "Voucher is not found");
            }
            if(vouch.CreatedDate > voucher.ExpiredDate)
            {
                return (false, null,"Expired date must be greater than created date");
            }

            vouch.ExpiredDate = voucher.ExpiredDate;
            vouch.VoucherValue = voucher.VoucherValue;
            vouch.VoucherCode = voucher.VoucherCode;
            vouch.VoucherTypeId = voucher.VoucherTypeId;
            vouch.MaxApply = voucher.MaxApply;

            await _context.SaveChangesAsync();

            return (true, vouch, "");
           
            
        }
    }
}
