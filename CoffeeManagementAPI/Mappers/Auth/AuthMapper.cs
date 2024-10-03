using Azure.Identity;
using CoffeeManagementAPI.DTOs.Staff;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Mappers.Auth
{
    public static class AuthMapper
    {
        public static Staff toStaffFromRegister(this RegisterStaffDTO staffDTO)
        {
            return new Staff
            {
                StaffName = staffDTO.StaffName,
                Username = staffDTO.Username,
                Password = staffDTO.Password,
                IsAdmin = staffDTO.IsAdmin,
            };
        }

        public static StaffDTO toStaffDTO (this Staff staff)
        {
            return new StaffDTO
            {
                StaffName = staff.StaffName,
                Username = staff.Username,
                StaffId = staff.StaffId,
                IsAdmin = staff.IsAdmin
            };
        }

    }
}
