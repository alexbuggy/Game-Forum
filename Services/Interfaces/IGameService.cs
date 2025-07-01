using GameForum.Models;
using GameForum.Models.Enums;
namespace GameForum.Services.Interfaces
{
    public interface IGameService
    {
        IEnumerable<Game> GetAll();
        Game GetById(int id);
        Game GetByIdWithPosts(int id);
        IEnumerable<Game> GetTop();
        IEnumerable<Game> GetByCategory(GameCategory category);
        IEnumerable<Game> GetAllWithCategories();
        void AddGame(Game game);
        void UpdateRating(int rating,int gameId);
    }
}
