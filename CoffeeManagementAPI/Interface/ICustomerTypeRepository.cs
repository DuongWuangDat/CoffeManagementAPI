using CoffeeManagementAPI.DTOs.Customer;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface ICustomerTypeRepository
    {
        Task<bool> CreateNewCustomerType(CustomerType customerType);

        Task<bool> UpdateCustomerType(CustomerType customerType, int id);

        Task<bool> DeleteCustomerType(int id);

        Task<List<CustomerTypeDTO>> GetAllCustomerType();

    }
}
