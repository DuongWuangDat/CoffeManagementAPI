using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoffeeManagementAPI.Services
{
    public class TokenService : ITokenService
    {
        public IConfiguration _config { get; set; }
        SymmetricSecurityKey _key { get; set; }
        ITokenRepository _tokenRepository { get; set; }

        TokenValidationParameters _validationParameters { get; set; }
        public TokenService(IConfiguration config, ITokenRepository tokenRepository)
        {
               _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
            _tokenRepository = tokenRepository;
            _validationParameters = new TokenValidationParameters 
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_config["JWT:SigningKey"])),
                ValidIssuer = _config["JWT:Issuer"],
                ValidAudience = _config["JWT:Aud"],
            };
        }

        public string GenerateAccessToken(Staff staff)
        {
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var claim = new List<Claim>
            {
            new Claim(JwtRegisteredClaimNames.Sub, staff.StaffId.ToString()),
            new Claim(JwtRegisteredClaimNames.Typ, "Access")

            };
            if(staff.IsAdmin)
            {
                claim.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            claim.Add(new Claim(ClaimTypes.Role, "User"));

            var accessToken = new JwtSecurityToken(
                claims: claim, 
                issuer: _config["JWT:Issuer"], 
                audience: _config["JWT:Aud"],
                signingCredentials: cred,
                expires: DateTime.Now.AddHours(1)
                );

            return new JwtSecurityTokenHandler().WriteToken( accessToken );
        }


        public async Task<string> GenerateRefreshToken(Staff staff)
        {
            var securityKey = _key;
            var cred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub , staff.StaffId.ToString()),
                new Claim(JwtRegisteredClaimNames.Typ, "Refresh")

            };
            if (staff.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            claims.Add(new Claim(ClaimTypes.Role, "Staff"));

            var refreshToken = new JwtSecurityToken(
                claims: claims,
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Aud"],
                expires: DateTime.Now.AddDays(30),
                signingCredentials: cred
                );
            await _tokenRepository.CreateToken(new JwtSecurityTokenHandler().WriteToken(refreshToken));
            return new JwtSecurityTokenHandler().WriteToken ( refreshToken );
        }

        public async Task<bool> IsValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, _validationParameters, out var validatedToken);

            if(validatedToken == null)
            {
                return false;
            }

            var typeToken = principal.FindFirst(JwtRegisteredClaimNames.Typ)?.Value;
            Console.WriteLine(typeToken);

            if(typeToken == "Refresh")
            {
                bool isRevoked = await _tokenRepository.IsTokenIsRevoked(token);
                return isRevoked;
            }
            
            return true;
        }

        public string RefreshThisToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var principal =tokenHandler.ValidateToken(token, _validationParameters, out SecurityToken validatedToken);
            var userID = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = principal.FindFirst(ClaimTypes.Role)?.Value;

            if(userID == null || userRole == null) { return ""; }

            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var claim = new List<Claim>
            {
            new Claim(JwtRegisteredClaimNames.Sub, userID),
            new Claim(JwtRegisteredClaimNames.Typ, "Access")

            };
            
            claim.Add(new Claim(ClaimTypes.Role, userRole));

            var accessToken = new JwtSecurityToken(
                claims: claim,
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Aud"],
                signingCredentials: cred,
                expires: DateTime.Now.AddHours(1)
                );

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }
    }
}
