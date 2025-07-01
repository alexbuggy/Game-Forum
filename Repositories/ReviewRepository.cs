using GameForum.Data;
using GameForum.Models;
using GameForum.Repositories.Interfaces;

namespace GameForum.Repositories
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(GameForumContext gameforumContext) : base(gameforumContext)
        {
        }
    }
}
