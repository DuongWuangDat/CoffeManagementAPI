using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.DTOs.Tables;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface ITableRepository
    {
        Task<IEnumerable<TableDTO>> GetTables();
        Task<IEnumerable<TableDTO>> GetTableByFloorID(int floorID);
        Task<(bool, string)> CreateNewTable(Table newTable);

        Task<(bool, string)> DeleteTable(int id);

        Task<(bool, string)> UpdateTable (UpdateTableDTO newTable, int id);

        Task<(bool, string)> UpdateStatusTable(UpdateStatusTableDTO newStatus, int id);

        Task<(bool, string)> BookTable(BookingTableDTO bookingTable);
        Task<(bool, string)> EndTable(int id);

        Task<IEnumerable<BillDTO>> GetBillFromBookingTable(int tableId);

    }
}
