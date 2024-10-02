using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CoffeeManagementAPI.Model
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }

        public string StaffName { get; set; } = string.Empty;

        public bool IsAdmin { get; set; } = false;
        public string Username { get; set; }= string.Empty;
        public string Password { get; set; }= string.Empty;
        public List<Bill> Bills { get; set; } = new List<Bill>();

    }
}
