using CoffeeManagementAPI.DTOs.Customer;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Mappers.Cus
{
    public static class CustomerMapper
    {

        public static CustomerDTO toCustomerDTO (this Customer customer)
        {
            return new CustomerDTO
            {
                CustomerID = customer.CustomerID,
                CustomerName = customer.CustomerName,
                PhoneNumber = customer.PhoneNumber,
                CustomerType = customer.CustomerType?.toCustomerTypeDTO(),
                Revenue = customer.Revenue,
                Email = customer.Email,
            };
        }

        public static Customer toCustomerFromCreated (this CreateCustomerDTO createCustomerDTO)
        {


            return new()
            {
                CustomerName = createCustomerDTO.CustomerName,
                PhoneNumber = createCustomerDTO.PhoneNumber,
                Revenue = createCustomerDTO.Revenue,
                Email = createCustomerDTO.Email,
                
            };
        }

        public static Customer toCustomerFromUpdated (this UpdateCustomerDTO updateCustomerDTO)
        {
            return new()
            {
                CustomerName = updateCustomerDTO.CustomerName,
                PhoneNumber = updateCustomerDTO.PhoneNumber,
                Email = updateCustomerDTO.Email,
            };
        }
    }
}
