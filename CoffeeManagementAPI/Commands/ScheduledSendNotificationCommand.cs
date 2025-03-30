using CoffeeManagementAPI.Interface.CommandInterface;

namespace CoffeeManagementAPI.Commands
{
    public class ScheduledSendNotificationCommand : INotificationCommand
    {
        private readonly INotificationCommand _innerCommand;
        private readonly TimeSpan _delay;
        private readonly CancellationTokenSource _cts;

        public ScheduledSendNotificationCommand(INotificationCommand innerCommand, DateTime scheduledTime)
        {
            _innerCommand = innerCommand;
            _delay = scheduledTime > DateTime.Now
                     ? scheduledTime - DateTime.Now
                     : TimeSpan.Zero;
            _cts = new CancellationTokenSource();
        }

        public async Task<(bool, string)> ExecuteAsync()
        {
            try
            {           
                await Task.Delay(_delay, _cts.Token);
                return await _innerCommand.ExecuteAsync();
            }
            catch (TaskCanceledException)
            {
                return (false, "Tác vụ gửi thông báo đã bị hủy.");
            }
        }

        public void Cancel()
        {
            _cts.Cancel();
        }
    }
}
