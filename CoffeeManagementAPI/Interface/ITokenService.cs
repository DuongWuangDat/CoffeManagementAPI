using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface ITokenService
    {

        public string GenerateAccessToken(Staff staff);
        public  Task<string> GenerateRefreshToken(Staff staff);

        public bool IsValidateToken (string token);
    }
}
