﻿using CoffeeManagementAPI.Data;
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
        ApplicaitonDBContext _context;

        public CustomerRepository(ApplicaitonDBContext context) 
        {
            _context = context;
        }

        public async Task<bool> CreateNewCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomer(int i)
        {
            var cus = await _context.Customers.FirstOrDefaultAsync(p=>p.CustomerID==i);
            if (cus == null) { return false; }
            _context.Customers.Remove(cus);

            await _context.SaveChangesAsync();
            return true;
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

        public Task<List<CustomerDTO>> GetCustomerPagination(PaginationObject pagination)
        {
            throw new NotImplementedException();
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
