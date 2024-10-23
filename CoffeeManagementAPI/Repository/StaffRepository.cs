using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Staff;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Auth;
using CoffeeManagementAPI.Model;
using CoffeeManagementAPI.QueryObject;
using CoffeeManagementAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class StaffRepository : IStaffRepository
    {

        ITokenService _tokenService;
        PasswordHasher<Staff> _passwordHasher;
        ApplicationDBContext _context;
        public StaffRepository(ITokenService tokenService, ApplicationDBContext context)
        {
            _tokenService = tokenService;
            _passwordHasher = new PasswordHasher<Staff>();
            _context = context;
        }

        public async Task<bool> DeleteStaff(int i)
        {
            var staff = await _context.Staff.FirstOrDefaultAsync(s => s.StaffId == i);
            if (staff == null)
            {
                return false;
            }
            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
            return true;
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

        public async Task<List<StaffDTO>> GetAllStaff()
        {
            var staffList = await _context.Staff.Select(s=> s.toStaffDTO()).ToListAsync();

            return staffList;
        }

        public async Task<List<StaffDTO>> GetStaffPagination(PaginationObject pagination)
        {
            var staffSelectable = _context.Staff.Select(s => s.toStaffDTO()).AsQueryable();

            var staffList = await staffSelectable.Skip(pagination.pageSize * (pagination.page -1)).Take(pagination.pageSize).ToListAsync();

            return staffList;
        }

        public async Task<bool> RegisterStaff(Staff staff)
        {
            var isUser = await _context.Staff.FirstOrDefaultAsync(s=> s.Username == staff.Username);
            if (isUser != null)
            {
                return false;
            }
            staff.Password = _passwordHasher.HashPassword(staff, staff.Password);
            await _context.Staff.AddAsync(staff);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(bool,Staff?)> UpdateStaff(Staff staff, int i)
        {
            var staffF = await _context.Staff.FirstOrDefaultAsync(s => s.StaffId == i);
            if (staffF == null)
            {
                return (false,null);
            }
            staffF.StaffName = staff.StaffName;
            staffF.Username = staff.Username;
            await _context.SaveChangesAsync();

            return (true, staffF);

        }
    }
}
