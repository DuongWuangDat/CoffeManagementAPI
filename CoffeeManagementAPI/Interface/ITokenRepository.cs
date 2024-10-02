namespace CoffeeManagementAPI.Interface
{
    public interface ITokenRepository
    {

        public Task CreateToken(string token);
    }
}
