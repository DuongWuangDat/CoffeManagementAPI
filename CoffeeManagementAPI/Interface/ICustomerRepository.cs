using CoffeeManagementAPI.DTOs.Customer;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface ICustomerRepository
    {

        Task CreateNewCustomer(Customer customer);

        Task DeleteCustomer(int i);

        Task UpadateCustomer(Customer customer, int id);

        Task<List<CustomerDTO>> GetAllCustomer();

        Task<CustomerDTO> GetCustomerById(int id);

    }
}
