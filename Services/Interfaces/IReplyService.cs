using GameForum.Models;

namespace GameForum.Services.Interfaces
{
    public interface IReplyService
    {

        void AddReply(Reply reply);
        void LoadRepliesRecursively(Reply reply);
    }
}
