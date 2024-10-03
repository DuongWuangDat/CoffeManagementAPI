using CoffeeManagementAPI.Model;
using System.IdentityModel.Tokens.Jwt;

namespace CoffeeManagementAPI.Interface
{
    public interface ITokenService
    {

        public string GenerateAccessToken(Staff staff);
        public  Task<string> GenerateRefreshToken(Staff staff);

        public Task<bool> IsValidateToken (string token);

        public string RefreshThisToken(string token);
    }
}
