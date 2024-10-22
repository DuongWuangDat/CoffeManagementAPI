namespace CoffeeManagementAPI.Interface
{
    public interface ISendMailService
    {
        Task<(bool, string)> SendMail(string email, string code);

    }
}
