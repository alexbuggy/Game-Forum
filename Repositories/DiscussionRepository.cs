using GameForum.Data;
using GameForum.Models;
using GameForum.Repositories.Interfaces;

namespace GameForum.Repositories
{
    public class DiscussionRepository : RepositoryBase<Discussion>, IDiscussionRepository
    {
        public DiscussionRepository(GameForumContext gameforumContext) : base(gameforumContext)
        {
        }
    }
}
