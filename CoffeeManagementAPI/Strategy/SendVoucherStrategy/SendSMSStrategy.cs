using CoffeeManagementAPI.Interface.StrategyInterface;

namespace CoffeeManagementAPI.Strategy.SendVoucherStrategy
{
    public class SendSMSStrategy : ISendVoucherStrategy
    {
        public Task<(bool, string)> SendVoucher(string email, string code)
        {
            throw new NotImplementedException();
        }
    }
}
