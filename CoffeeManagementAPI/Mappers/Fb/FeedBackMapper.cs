using CoffeeManagementAPI.DTOs.Feedback;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Mappers.Fb
{
    public static class FeedBackMapper
    {

        public static FeedBackDTO toFeedBackDTO (this Feedback feedback)
        {
            return new()
            {
                Content = feedback.Content,
                Email = feedback.Email,
                Fullname = feedback.Fullname,
                Phonenumber = feedback.Phonenumber,
                StarNumber = feedback.StarNumber,
                FeedbackId = feedback.FeedbackID
                
            };
        }

        public static Feedback toFeedbackFromCreated (this CreatedFeedBackDTO createdFeedBackDTO)
        {
            return new()
            {
                Content = createdFeedBackDTO.Content,
                Email = createdFeedBackDTO.Email,
                Fullname= createdFeedBackDTO.Fullname,
                Phonenumber= createdFeedBackDTO.Phonenumber,
                StarNumber= createdFeedBackDTO.StarNumber
            };
        } 

    }
}
