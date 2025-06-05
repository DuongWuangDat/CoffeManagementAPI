using CoffeeManagementAPI.Interface.HandlerInterface;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Handler.BillHandler
{
    public abstract class BaseBillHandler : IBillHandler
    {
        private IBillHandler? _next;

        public IBillHandler SetNext(IBillHandler handler)
        {
            _next = handler;
            return handler;
        }

        public virtual async Task<(bool, string)> HandleAsync(Bill bill)
        {
            if (_next != null)
            {
                return await _next.HandleAsync(bill);
            }
            return (true, "");
        }
    }
}
