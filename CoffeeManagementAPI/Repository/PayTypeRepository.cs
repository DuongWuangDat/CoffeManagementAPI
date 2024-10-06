using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class PayTypeRepository : IPayTypeRepository
    {
        ApplicaitonDBContext _context;
        public PayTypeRepository(ApplicaitonDBContext context)
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
