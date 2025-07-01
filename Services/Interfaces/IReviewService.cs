using GameForum.Models.Enums;
using GameForum.Models;

namespace GameForum.Services.Interfaces
{
    public interface IReviewService
    {
        IEnumerable<Review> GetAll();
        Review GetById(int id);
        IEnumerable<Review> GetTop();
        IEnumerable<Review> GetReviewsByGameId(int gameId);
        IEnumerable<Review> GetReviewsByUserId(string userId);
        void AddReview(Review review);
        Review AddOrUpdateReview(string userId, int gameId, string content, int rating);

    }
}
