using CoffeeManagementAPI.DTOs.Tables;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Mappers.Tble
{
    public static class FloorMapper
    {
        public static FloorDTO toFloorDTO (this Floor floor) {
            return new()
            {
                FloorID = floor.FloorID,
                FloorNumber = floor.FloorNumber,
            };
        }
        
    }
}
