using CoffeeManagementAPI.Model;
using System.IdentityModel.Tokens.Jwt;

namespace CoffeeManagementAPI.Interface
{
    public interface ITokenService
    {

        string GenerateAccessToken(Staff staff);
        Task<string> GenerateRefreshToken(Staff staff);

        Task<bool> IsValidateToken (string token);

        string RefreshThisToken(string token);
    }
}
