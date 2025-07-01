using GameForum.Models;

namespace GameForum.Repositories.Interfaces
{
    public interface IGameRepository : IRepositoryBase<Game>
    {
        public Game GetByIdWithPosts(int id);
    }
}
