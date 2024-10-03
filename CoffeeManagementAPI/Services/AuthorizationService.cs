using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Model;
using Microsoft.AspNetCore.Identity;

namespace CoffeeManagementAPI.Services
{
    public class AuthorizationService : IAuthorization
    {
        PasswordHasher<Staff> _passwordHasher;
        public AuthorizationService()
        {
            _passwordHasher = new PasswordHasher<Staff>();
        }
        public string HashPassword(Staff staff, string password)
        {
            var passwordHash = _passwordHasher.HashPassword(staff, password);
            return passwordHash;
        }

        public bool VerifyPassword(Staff staff, string hashPassword, string password)
        {
            var checkPassword = _passwordHasher.VerifyHashedPassword(staff, hashPassword, password);
            if(checkPassword == PasswordVerificationResult.Success)
            {
                return true;
            }
            return false;
        }
    }
}
