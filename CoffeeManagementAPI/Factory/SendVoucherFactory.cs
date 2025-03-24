using CoffeeManagementAPI.Interface.StrategyInterface;
using CoffeeManagementAPI.Strategy.SendVoucherStrategy;

namespace CoffeeManagementAPI.Factory
{
    public class SendVoucherFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SendVoucherFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ISendVoucherStrategy GetSendVoucher(string sendVoucherType)
        {
            return sendVoucherType switch
            {
                "EMAIL" => _serviceProvider.GetRequiredService<SendEmailStrategy>(),
                "SMS" => _serviceProvider.GetRequiredService<SendSMSStrategy>(),
                _ => throw new NotSupportedException()
            };
         
        }

    }
}
