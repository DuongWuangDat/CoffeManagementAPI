namespace CoffeeManagementAPI.Interface.CommandInterface
{
    public interface INotificationCommand
    {
        Task<(bool,string)> ExecuteAsync();
    }
}
