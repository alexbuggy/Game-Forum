using GameForum.Models.Enums;

namespace GameForum.Services.Interfaces
{
    public interface IVoteService
    {
        bool ToggleVote(int postId, string userId, VoteType voteType);
    }
}
