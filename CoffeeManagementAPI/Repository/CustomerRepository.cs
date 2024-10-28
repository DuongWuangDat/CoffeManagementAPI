using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Customer;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Cus;
using CoffeeManagementAPI.Model;
using CoffeeManagementAPI.QueryObject;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        ApplicationDBContext _context;

        public CustomerRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<(bool,string)> CreateNewCustomer(Customer customer)
        {
            var isExist = await _context.Customers.AnyAsync(c=>c.PhoneNumber == customer.PhoneNumber);
            if (isExist)
            {
                return (false, "Phonenumber is existed");
            }
            var isEmailExist = await _context.Customers.AnyAsync(c=> c.Email == customer.Email);
            if (isEmailExist)
            {
                return (false, "Email is existed");
            }
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return (true,"");
        }

        public async Task<(bool,string)> DeleteCustomer(int i)
        {
            var cus = await _context.Customers.FirstOrDefaultAsync(p=>p.CustomerID==i);
            if (cus == null) { return (false, "CustomerID is not found"); }
            _context.Customers.Remove(cus);

            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<List<CustomerDTO>> GetAllCustomer()
        {
            var cusList = await _context.Customers.Include(c=> c.CustomerType).Select(p=> p.toCustomerDTO()).ToListAsync();
            return cusList;
        }

        public async Task<CustomerDTO?> GetCustomerById(int id)
        {
            var cus = await _context.Customers.Include(c=>c.CustomerType).FirstOrDefaultAsync(p=> p.CustomerID==id);
            if (cus == null) { return null; }
            return cus.toCustomerDTO();
        }

        public async Task<CustomerDTO?> GetCustomerByPhonenumber(string phonenumber)
        {
            var cus = await _context.Customers.Where(c=> c.PhoneNumber == phonenumber).FirstOrDefaultAsync();

            if(cus == null)
            {
                return null;
            }

            return cus.toCustomerDTO();
        }

        public async Task<List<CustomerDTO>> GetCustomerPagination(PaginationObject pagination)
        {
            var cusSelectable = _context.Customers.Select(c=> c.toCustomerDTO()).AsQueryable();

            var cusList = await cusSelectable.Skip(pagination.pageSize * (pagination.page-1)).Take(pagination.pageSize).ToListAsync();

            return cusList;
        }

        public async Task<(bool,Customer?)> UpadateCustomer(Customer customer, int id)
        {
            var cus = await _context.Customers.FirstOrDefaultAsync(p=> p.CustomerID==id);

            if(cus == null) { return (false,null); }

            cus.PhoneNumber = customer.PhoneNumber;
            cus.CustomerName = customer.CustomerName;
            await _context.SaveChangesAsync();
            return (true,cus);
        } 
    }
}
