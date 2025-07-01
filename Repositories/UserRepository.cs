using GameForum.Data;
using GameForum.Models;
using GameForum.Repositories.Interfaces;

namespace GameForum.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(GameForumContext gameforumContext) : base(gameforumContext)
        {
        }
    }
}
