using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.Model;
using System;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Handler.BillHandler
{
    public class CustomerHandler : BaseBillHandler
    {
        private ApplicationDBContext _context;

        public CustomerHandler(ApplicationDBContext context)
        {
            _context = context;
        }

        public override async Task<(bool, string)> HandleAsync(Bill bill)
        {
            if (bill.CustomerId != null)
            {
                var cus = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == bill.CustomerId);
                if (cus == null)
                    return (false, "CustomerID is not found");

                cus.Revenue += bill.TotalPrice;

                var customerType = await _context.CustomerTypes
                    .OrderByDescending(c => c.BoundaryRevenue)
                    .FirstOrDefaultAsync(c => c.BoundaryRevenue <= cus.Revenue);

                if (customerType != null)
                    cus.CustomerTypeId = customerType.CustomerTypeID;
            }

            return await base.HandleAsync(bill);
        }
    }

}
