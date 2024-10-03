using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface IAuthorization
    {

        public string HashPassword(Staff staff,string password);
        public bool VerifyPassword(Staff staff,string hashPassword, string password);
    }
}
