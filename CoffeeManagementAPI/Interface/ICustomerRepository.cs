using CoffeeManagementAPI.DTOs.Customer;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface ICustomerRepository
    {

        Task<bool> CreateNewCustomer(Customer customer);

        Task<bool> DeleteCustomer(int i);

        Task<(bool, Customer?)> UpadateCustomer(Customer customer, int id);

        Task<List<CustomerDTO>> GetAllCustomer();

        Task<CustomerDTO?> GetCustomerById(int id);

    }
}
