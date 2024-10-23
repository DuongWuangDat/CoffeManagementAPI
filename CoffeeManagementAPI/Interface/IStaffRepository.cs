using CoffeeManagementAPI.DTOs.Staff;
using CoffeeManagementAPI.Model;
using CoffeeManagementAPI.QueryObject;

namespace CoffeeManagementAPI.Interface
{
    public interface IStaffRepository
    {

        public Task<bool> RegisterStaff(Staff staff);
        public Task<Staff?> FindUser(string username);

        Task<List<StaffDTO>> GetAllStaff();

        Task<(bool,Staff?)> UpdateStaff(Staff staff, int i);

        Task<bool> DeleteStaff(int i);

        Task<List<StaffDTO>> GetStaffPagination(PaginationObject pagination);



    }
}
