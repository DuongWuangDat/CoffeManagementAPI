using CoffeeManagementAPI.DTOs.Staff;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface IStaffRepository
    {

        public Task RegisterStaff(Staff staff);
        public Task<Staff?> FindUser(string username);

    }
}
