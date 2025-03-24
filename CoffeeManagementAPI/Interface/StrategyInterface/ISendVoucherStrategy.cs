namespace CoffeeManagementAPI.Interface.StrategyInterface
{
    public interface ISendVoucherStrategy
    {

        Task<(bool, string)> SendVoucher(string email, string code);
    }
}
