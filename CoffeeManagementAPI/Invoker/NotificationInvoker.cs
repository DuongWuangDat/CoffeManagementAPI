using CoffeeManagementAPI.Interface.CommandInterface;

namespace CoffeeManagementAPI.Invoker
{
    public class NotificationInvoker
    {
        private readonly Queue<INotificationCommand> _commands = new();

        public void AddCommand(INotificationCommand command)
        {
            _commands.Enqueue(command);
        }

        public async Task<(bool, string)> ProcessCommandsAsync()
        {
            while (_commands.Count > 0)
            {
                var command = _commands.Dequeue();
                var (success, err) = await command.ExecuteAsync();
                if (!success)
                {
                    Console.WriteLine("⚠️ Một lệnh đã thất bại, dừng hàng đợi!");
                    return (false, err);
                }
            }
            return (true, "");
        }
    }
}
