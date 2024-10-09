using CoffeeManagementAPI.DTOs.Staff;
using CoffeeManagementAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CoffeeManagementAPI.Mappers.Sta
{
    public static class StaffMapper
    {

        public static Staff toStaffFromUpdate(this UpdatedStaffDTO updatedStaffDTO)
        {
            return new Staff
            {
                Username = updatedStaffDTO.Username,
                StaffName = updatedStaffDTO.StaffName,
            };
        }

    }
}
