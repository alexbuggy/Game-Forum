using GameForum.Repositories.Interfaces;
using GameForum.Models;
using GameForum.Repositories;
using GameForum.Data;

namespace GameForum.Repositories
{
    public class VoteRepository : RepositoryBase<Vote>, IVoteRepository
    {
        public VoteRepository(GameForumContext gameforumContext) : base(gameforumContext)
        {
        }
    }
}
