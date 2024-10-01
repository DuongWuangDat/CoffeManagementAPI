using Microsoft.AspNetCore.Identity;

namespace CoffeeManagementAPI.Model
{
    public class AppUser : IdentityUser
    {
        public string StaffName { get; set; } = string.Empty;

                
    }
}
