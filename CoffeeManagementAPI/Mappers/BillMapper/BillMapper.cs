using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.Mappers.Auth;
using CoffeeManagementAPI.Mappers.Cus;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Mappers.BillMapper
{
    public static class BillMapper
    {

         public static BillDTO toBillDTO (this Bill bill)
        {
            return new BillDTO
            {
                BillId = bill.BillId,
                Customer = bill.Customer?.toCustomerDTO(),
                PayType = bill.PayType,
                Staff = bill.Staff?.toStaffDTO(),
                Status = bill.Status,
                TotalPrice = bill.TotalPrice,
                VoucherTypeIndex = bill.VoucherTypeIndex,
                VoucherValue = bill.VoucherValue,
                BillDetailDTOs = bill.BillDetails.Select(x=> x.toBillDetailDTO()).ToList(),
                CreatedAt = bill.DateTime
            };
        }


        public static BillFromGetAllDTO toBillFromGetAllDTO (this Bill bill)
        {
            return new BillFromGetAllDTO
            {
                BillId = bill.BillId,
                CreatedAt = bill.DateTime,
                Status = bill.Status,
                TotalPrice = bill.TotalPrice,
            };
        }
    }
}
