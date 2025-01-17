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
        public async Task<(bool, string)> CreateFeedBack(CreatedFeedBackDTO newFb)
        {
            var prodList = newFb.listProdFb;
            foreach(var prod in prodList){
                var product = await _context.Products.FirstOrDefaultAsync(p=> p.ProductID ==  prod.ProductID);
                if(product == null)
                {
                    return (false, "ProductId not found");
                }
                var person = product.RatingPerson +1;
                product.RatingPerson = person;
                var prevProd = product.AverageStar;
                var average = (prevProd + prod.Star) / person;
                product.AverageStar=average;
                await _context.SaveChangesAsync();
            }
            var fb = newFb.toFeedbackFromCreated();
            await _context.Feedbacks.AddAsync(fb);
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

            var reverseList = fbList.AsEnumerable().Reverse();
            return fbList;
        }
    }
}
