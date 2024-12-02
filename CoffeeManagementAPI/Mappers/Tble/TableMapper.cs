using CoffeeManagementAPI.DTOs.Tables;
using CoffeeManagementAPI.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeManagementAPI.Mappers.Tble
{
    public static class TableMapper
    {

        public static Floor toFloorFromCreated (this CreateFloorDTO createFloorDTO)
        {
            return new()
            {
                FloorNumber = createFloorDTO.FloorNumber,
            };
        }

        public static TableDTO toTableDTO (this Table table)
        {
            return new()
            {
                Floor = table.Floor,
                FloorId = table.FloorId,
                Status = table.Status,
                TableID = table.TableID,
                TableNumber = table.TableNumber,

            };
        }

        public static Table toTableFromCreate(this CreateTableDTO createTableDTO)
        {
            return new()
            {
                TableNumber = createTableDTO.TableNumber,
                FloorId = createTableDTO.FloorId,
                
            };
        }

        public static BookingTable toBookingTable (this BookingTableDTO bookingTableDTO)
        {
            return new()
            {
                BillId = bookingTableDTO.BillId,
                TableId = bookingTableDTO.TableId,
            };
        }

    }
}
