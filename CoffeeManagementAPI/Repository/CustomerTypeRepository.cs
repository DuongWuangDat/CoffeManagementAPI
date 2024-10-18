using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Customer;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Cus;
using CoffeeManagementAPI.Model;
using CoffeeManagementAPI.QueryObject;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class CustomerTypeRepository : ICustomerTypeRepository
    {
        ApplicationDBContext _context;

        public CustomerTypeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateNewCustomerType(CustomerType customerType)
        {
            await _context.CustomerTypes.AddAsync(customerType);

            var cusPrev = await _context.CustomerTypes.OrderBy(c=> c.BoundaryRevenue).Where(c=> c.BoundaryRevenue > customerType.BoundaryRevenue).FirstOrDefaultAsync();
            if(cusPrev == null)
            {
                await _context.Customers
               .Where(c => c.Revenue >= customerType.BoundaryRevenue)
               .ExecuteUpdateAsync(setter => setter.SetProperty(b => b.CustomerTypeId, customerType.CustomerTypeID));
            }
            else
            {
                await _context.Customers
                .Where(c => c.Revenue >= customerType.BoundaryRevenue && c.Revenue < cusPrev.BoundaryRevenue)
                .ExecuteUpdateAsync(setter => setter.SetProperty(b => b.CustomerTypeId, customerType.CustomerTypeID));
            }
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCustomerType(int id)
        {
            var cusType = await _context.CustomerTypes.Where(c => c.CustomerTypeID == id).FirstOrDefaultAsync();
            if (cusType == null)
            {
                return false;
            }

            var cusAfter = await _context.CustomerTypes.OrderByDescending(c=> c.BoundaryRevenue).Where(c=> c.BoundaryRevenue < cusType.BoundaryRevenue ).FirstOrDefaultAsync();

            if (cusAfter == null) {
                _context.CustomerTypes.Remove(cusType);
            }
            else
            {
                await _context.CustomerTypes.Where(c=> c.CustomerTypeID == id).ExecuteUpdateAsync(setter=> setter.SetProperty(c=> c.CustomerTypeID, cusAfter.CustomerTypeID));
                _context.CustomerTypes.Remove(cusType);
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<CustomerTypeDTO>> GetAllCustomerType()
        {
            var customerTypes = await _context.CustomerTypes.OrderByDescending(c=> c.BoundaryRevenue).Select(c=> c.toCustomerTypeDTO()).ToListAsync();
            return customerTypes;
        }

        public async Task<bool> UpdateCustomerType(CustomerType customerType,int id)
        {
            var cusType = await _context.CustomerTypes.Where(c=> c.CustomerTypeID == id).FirstOrDefaultAsync();
            if(cusType == null)
            {
                return false;
            }

            if(cusType.BoundaryRevenue != customerType.BoundaryRevenue) {
                var cusPrev = await _context.CustomerTypes.OrderBy(c => c.BoundaryRevenue).Where(c => c.BoundaryRevenue > customerType.BoundaryRevenue && c.CustomerTypeID != id).FirstOrDefaultAsync();
                if (cusPrev == null)
                {
                    await _context.Customers
                   .Where(c => c.Revenue >= customerType.BoundaryRevenue)
                   .ExecuteUpdateAsync(setter => setter.SetProperty(b => b.CustomerTypeId, customerType.CustomerTypeID));
                }
                else
                {
                    await _context.Customers
                    .Where(c => c.Revenue >= customerType.BoundaryRevenue && c.Revenue < cusPrev.BoundaryRevenue)
                    .ExecuteUpdateAsync(setter => setter.SetProperty(b => b.CustomerTypeId, customerType.CustomerTypeID));
                }
                cusType.BoundaryRevenue = customerType.BoundaryRevenue;

            }

            cusType.DiscountValue = customerType.DiscountValue;
            cusType.CustomerTypeName = customerType.CustomerTypeName;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
