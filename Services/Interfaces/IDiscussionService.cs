using GameForum.Models;

namespace GameForum.Services.Interfaces
{
    public interface IDiscussionService
    {
        IEnumerable<Discussion> GetAll();
        Discussion GetById(int id);
        IEnumerable<Discussion> GetDiscussionByGameId(int gameId);
        IEnumerable<Discussion> GetDiscussionByUserId(string userId);
        void AddDiscussion(Discussion discussion);
    }
}
