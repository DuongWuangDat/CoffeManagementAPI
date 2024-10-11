using CoffeeManagementAPI.DTOs.Bill;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Mappers.BillMapper
{
    public static class BillDetailMapper
    {

        public static BillDetailDTO toBillDetailDTO (this BillDetail billDetail)
        {
            return new BillDetailDTO
            {
                ProductCount = billDetail.ProductCount,
                ProductName = billDetail.ProductName,
                ProductPrice = billDetail.ProductPrice,
                TotalPriceDtail = billDetail.TotalPriceDtail
            };
        }

        public static BillDetail toBillDetailFromCreated (this CreateBillDetailDTO createBillDetailDTO)
        {
            return new BillDetail()
            {
                ProductCount = createBillDetailDTO.ProductCount,
                ProductId = createBillDetailDTO.ProductId,
                ProductName= createBillDetailDTO.ProductName,
                ProductPrice= createBillDetailDTO.ProductPrice,
                TotalPriceDtail = createBillDetailDTO.TotalPriceDtail
            };
        }


    }
}
