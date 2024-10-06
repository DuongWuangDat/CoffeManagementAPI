﻿using CoffeeManagementAPI.DTOs.Voucher;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Mappers.VoucherMapper
{
    public static class VoucherMapper
    {

        public static VoucherDTO toVoucherDTO(this Voucher voucher)
        {
            return new()
            {
                CreatedDate = voucher.CreatedDate,
                ExpiredDate = voucher.ExpiredDate,
                VoucherCode = voucher.VoucherCode,
                MaxApply = voucher.MaxApply,
                VoucherID = voucher.VoucherID,
                VoucherType = voucher.VoucherType,
                VoucherValue = voucher.VoucherValue,
            };
        }

        public static Voucher toVoucherFromCreated(this CreatedVoucherDTO voucher)
        {
            return new()
            {
                CreatedDate = voucher.CreatedDate,
                ExpiredDate = voucher.ExpiredDate,
                MaxApply = voucher.MaxApply,
                VoucherCode = voucher.VoucherCode,
                VoucherID = voucher.VoucherID,
                VoucherTypeId = voucher.VoucherTypeId,
                VoucherValue = voucher.VoucherValue,
            };
        }
    }
}
