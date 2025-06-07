using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Handler.BillHandler
{
    public class VoucherHandler : BaseBillHandler
    {
        private  ApplicationDBContext _context;

        public VoucherHandler(ApplicationDBContext context)
        {
            _context = context;
        }

        public override async Task<(bool, string)> HandleAsync(Bill bill)
        {
            if (bill.VoucherId != null)
            {
                var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.VoucherID == bill.VoucherId && v.MaxApply > 0);

                if (voucher == null)
                    return (false, "VoucherID is not found");

                voucher.MaxApply--;
                bill.VoucherValue = voucher.VoucherValue;
                bill.VoucherTypeIndex = (int)voucher.VoucherTypeId;
            }

            return await base.HandleAsync(bill);
        }
    }

}
