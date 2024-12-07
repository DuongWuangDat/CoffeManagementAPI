using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.DTOs.Tables;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.BillMapper;
using CoffeeManagementAPI.Mappers.Tble;
using CoffeeManagementAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace CoffeeManagementAPI.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly ApplicationDBContext _context;

        public TableRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<(bool, string)> BookTable(BookingTableDTO bookingTable)
        {
            var table = await _context.Tables.FirstOrDefaultAsync(p=> p.TableID == bookingTable.TableId);
            var isBillExist = await _context.Bills.AnyAsync(b=> b.BillId == bookingTable.BillId);
            if(table == null)
            {
                return (false, "Table is not found");
            }
            if(!isBillExist) {
                return (false, "Bill is not found");
            }
            if(table.Status== "Booked" || table.Status== "Under repair")
            {
                return (false, "Table is occupied");
            }

            var bookTable =  bookingTable.toBookingTable();
            table.Status = "Booked";
            await _context.BookingTables.AddAsync(bookTable);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<(bool, string)> CreateNewTable(Table newTable)
        {
            newTable.Status = "Not booked";
            await _context.Tables.AddAsync(newTable);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<(bool, string)> DeleteTable(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if(table== null)
            {
                return (false, "Table is not found");
            }
            if(table.Status == "Booked")
            {
                return (false, "End this table before deleting it");
            }
            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
            return (true, "");

        }

        public async Task<(bool, string)> EndTable(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if(table == null)
            {
                return (false, "Table is not found");
            }
            var bookTableList = await _context.BookingTables.Where(bt=> bt.TableId == id).ToListAsync();
            table.Status = "Not booked";
            _context.BookingTables.RemoveRange(bookTableList);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<IEnumerable<BillDTO>> GetBillFromBookingTable(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            if (table == null)
            {
                return [];
            }
            var billList = await _context.BookingTables.Where(bt => bt.TableId == tableId).Include(bt=> bt.Bill).Include(b => b.Bill.BillDetails).Select(b=> b.Bill).ToListAsync();
            if (billList == null || billList.Count == 0)
            {
                return [];
            }
            var billDTOList = billList.Select(b => b.toBillDTO());
            return billDTOList;
        }

        public async Task<IEnumerable<TableDTO>> GetTableByFloorID(int floorID)
        {
            var tables = await _context.Tables.Where(t=> t.FloorId == floorID).Include(t=> t.Floor).Include(s=> s.TableType).Select(t => t.toTableDTO()).ToListAsync();
            return tables;
        }

        public async Task<IEnumerable<TableDTO>> GetTableByType(int tableType)
        {
            var tables = await _context.Tables.Where(t => t.TableTypeId == tableType).Include(t => t.Floor).Include(s => s.TableType).Select(t => t.toTableDTO()).ToListAsync();
            return tables;
        }

        public async Task<IEnumerable<TableDTO>> GetTables()
        {
            var tables = await _context.Tables.Include(t=> t.Floor).Include(t=> t.TableType).Select(t=> t.toTableDTO()).ToListAsync();
            return tables;
        }

        public async Task<(bool, string)> UpdateStatusTable(UpdateStatusTableDTO newStatus, int id)
        {
            var table =await _context.Tables.FindAsync(id);
            if (table == null)
            {
                return (false, "Table is not found");
            }
            if(table.Status == "Booked")
            {
                return (false, "Please end this table before updating it");
            }
            table.Status = newStatus.Status;
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<(bool, string)> UpdateTable(UpdateTableDTO newTable, int id)
        {
            bool flag = false;
            var table = await _context.Tables.FindAsync(id);
            if (table == null)
            {
                return (false, "Table is not found");
            }
            if(table.Status == "Booked")
            {
                flag = true;
            }
            else
            {
                table.Status = newTable.Status;
            }
            
            table.TableNumber = newTable.TableNumber;
            table.FloorId = newTable.FloorId;
            table.TableTypeId = newTable.TableTypeID;
            await _context.SaveChangesAsync();
            if (flag)
            {
                return (true, "Update status failed (Please end this table before updating it). Other field is updated successfully");
            }
            return (true, "Update successfully");
        }
    }
}
