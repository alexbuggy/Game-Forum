using GameForum.Data;
using GameForum.Models;
using GameForum.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameForum.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(GameForumContext gameforumContext) : base(gameforumContext)
        {

        }

        public Game GetByIdWithPosts(int id)
        {
            return GameForumContext.Games
                .Include(g => g.Reviews).ThenInclude(r => r.Author)
                .Include(g => g.Discussions).ThenInclude(d => d.Author)
                .FirstOrDefault(g => g.Id == id);
        }
    }
}

