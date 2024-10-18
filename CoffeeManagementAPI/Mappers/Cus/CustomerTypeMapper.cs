using CoffeeManagementAPI.DTOs.Customer;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Mappers.Cus
{
    public static class CustomerTypeMapper
    {

        public static CustomerTypeDTO toCustomerTypeDTO (this CustomerType customerType)
        {
            return new CustomerTypeDTO
            {
                BoundaryRevenue = customerType.BoundaryRevenue,
                CustomerTypeID = customerType.CustomerTypeID,
                CustomerTypeName = customerType.CustomerTypeName,
                DiscountValue = customerType.DiscountValue,
            };
        }

        public static CustomerType toCustomerFromCreate( this CreateCustomerTypeDTO createCustomerTypeDTO )
        {
            return new()
            {
                BoundaryRevenue = createCustomerTypeDTO.BoundaryRevenue,
                CustomerTypeName = createCustomerTypeDTO.CustomerTypeName,
                DiscountValue = createCustomerTypeDTO.DiscountValue,
            };
        }
    }
}
