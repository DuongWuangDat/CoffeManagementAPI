using CoffeeManagementAPI.Data;
using CoffeeManagementAPI.DTOs.Feedback;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Mappers.Fb;
using CoffeeManagementAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagementAPI.Repository
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private readonly ApplicationDBContext _context;

        public FeedBackRepository(ApplicationDBContext context) { 
            _context = context; 
        }
        public async Task<(bool, string)> CreateFeedBack(Feedback newFb)
        {
            await _context.Feedbacks.AddAsync(newFb);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<(bool, string)> DeleteFeedBack(int id)
        {
            var fb = await _context.Feedbacks.FirstOrDefaultAsync(p=> p.FeedbackID == id);
            if (fb == null)
            {
                return (false, "Feedback is not found");
            }
            _context.Feedbacks.Remove(fb);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<IEnumerable<FeedBackDTO>> GetAllFeedBack()
        {
            var fbList = await _context.Feedbacks.Select(p => p.toFeedBackDTO()).ToListAsync();

            return fbList;
        }
    }
}
