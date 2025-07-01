using GameForum.Data;
using GameForum.Models;
using GameForum.Repositories.Interfaces;

namespace GameForum.Repositories
{
    public class FavoriteGameRepository : RepositoryBase<FavoriteGame>, IFavoriteGameRepository
    {
        public FavoriteGameRepository(GameForumContext gameforumContext) : base(gameforumContext)
        {
        }
    }
}
