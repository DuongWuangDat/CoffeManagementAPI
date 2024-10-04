namespace CoffeeManagementAPI.Interface
{
    public interface ITokenRepository
    {

        Task CreateToken(string token);

        Task<bool> IsTokenIsRevoked (string token);
    }
}
