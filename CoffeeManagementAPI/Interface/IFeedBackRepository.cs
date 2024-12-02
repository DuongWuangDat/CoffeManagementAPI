using CoffeeManagementAPI.DTOs.Feedback;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Interface
{
    public interface IFeedBackRepository
    {

        Task<IEnumerable<FeedBackDTO>> GetAllFeedBack();

        Task<(bool, string)> CreateFeedBack(Feedback newFb);

        Task<(bool, string)> DeleteFeedBack(int id);



    }
}
