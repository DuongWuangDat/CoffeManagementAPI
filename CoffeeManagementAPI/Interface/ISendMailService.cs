namespace CoffeeManagementAPI.Interface
{
    public interface ISendMailService
    {
        Task<bool> SendMail(string email, string code);

    }
}
