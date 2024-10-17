using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class PayTypeRepository : IPayTypeRepository
    {
        ApplicationDBContext _context;
        public PayTypeRepository(ApplicationDBContext context)
        {
            _context = context;

        }
  
            
      
        public async Task<List<PayType>> GetAll()
        {
            var PayTypeList = await _context.PayTypes.ToListAsync();

            return PayTypeList;
        }
    }
}
