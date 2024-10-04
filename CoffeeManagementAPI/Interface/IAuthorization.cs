using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface IAuthorization
    {

        string HashPassword(Staff staff,string password);
        bool VerifyPassword(Staff staff,string hashPassword, string password);
    }
}
