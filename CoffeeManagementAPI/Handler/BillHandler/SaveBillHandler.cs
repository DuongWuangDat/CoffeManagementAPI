using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.Model;

using System;

namespace CoffeeManagementAPI.Handler.BillHandler
{
    public class SaveBillHandler : BaseBillHandler
    {
        private ApplicationDBContext _context;

        public SaveBillHandler(ApplicationDBContext context)
        {
            _context = context;
        }

        public override async Task<(bool, string)> HandleAsync(Bill bill)
        {
            await _context.AddAsync(bill);
            await _context.SaveChangesAsync();

            return (true, "");
        }
    }

}
