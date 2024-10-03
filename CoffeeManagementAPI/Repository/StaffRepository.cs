using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Staff;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Auth;
using CoffeeManagementAPI.Model;
using CoffeeManagementAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class StaffRepository : IStaffRepository
    {

        ITokenService _tokenService;
        PasswordHasher<Staff> _passwordHasher;
        ApplicaitonDBContext _context;
        public StaffRepository(ITokenService tokenService, ApplicaitonDBContext context)
        {
            _tokenService = tokenService;
            _passwordHasher = new PasswordHasher<Staff>();
            _context = context;
        }

        public async Task<Staff?> FindUser(string username)
        {
            Staff? staff = await _context.Staff.FirstOrDefaultAsync(x => x.Username == username);

            if(staff == null)
            {
                return null;
            }

            return staff;
        }

        public async Task RegisterStaff(Staff staff)
        {
                staff.Password = _passwordHasher.HashPassword(staff, staff.Password);
                await _context.Staff.AddAsync(staff);
                await _context.SaveChangesAsync();
        }
    }
}
