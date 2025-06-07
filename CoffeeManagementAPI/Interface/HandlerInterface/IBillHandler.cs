using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface.HandlerInterface
{
    public interface IBillHandler
    {
        IBillHandler SetNext(IBillHandler handler);
        Task<(bool, string)> HandleAsync(Bill bill);
    }
}
