using CoffeeManagementAPI.State.TableState;

namespace CoffeeManagementAPI.Factory
{
    public static class TableStateFactory
    {
        public static ITableState Create(string status)
        {
            return status switch
            {
                "Booked" => new BookedState(),
                "Under repair" => new UnderRepairState(),
                "Not booked" => new NotBookedState(),
                _ => throw new ArgumentException($"Unknown table status: {status}")
            };
        }
    }
}
