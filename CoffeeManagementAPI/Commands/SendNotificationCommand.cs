using CoffeeManagementAPI.Interface.CommandInterface;
using CoffeeManagementAPI.Interface.StrategyInterface;

namespace CoffeeManagementAPI.Commands
{
    public class SendNotificationCommand : INotificationCommand
    {
        private readonly ISendVoucherStrategy _strategy;
        private readonly string _recipient;
        private readonly string _message;

        public SendNotificationCommand(ISendVoucherStrategy strategy, string recipient, string message)
        {
            _strategy = strategy;
            _recipient = recipient;
            _message = message;
        }

        public async Task<(bool, string)> ExecuteAsync()
        {
            try
            {
                var (isSuccess, err) = await _strategy.SendVoucher(_recipient, _message);
                Console.WriteLine($"✅ Gửi thông báo thành công đến {_recipient}");
                return (isSuccess, err);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Gửi thông báo thất bại: {ex.Message}");
                return (false, ex.Message);
            }
        }
    }
}
