using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class TokenRepository : ITokenRepository
    {
        ApplicaitonDBContext _context;
        public TokenRepository(ApplicaitonDBContext context)
        {
               _context = context;
        }
        public async Task CreateToken(string token)
        {
            Token tokenCreated = new Token
            {
                TokenName = token,
                IsRevoked = false
            };

            await _context.Tokens.AddAsync(tokenCreated);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsTokenIsRevoked(string token)
        {
            var tokenFind = await _context.Tokens.FirstOrDefaultAsync(t=> t.TokenName == token);

            if (tokenFind == null)
            {
                return false;
            }
            return !tokenFind.IsRevoked;
        }
    }
}
