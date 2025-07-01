using GameForum.Models;

namespace GameForum.Services.Interfaces
{
    public interface IFavoriteGameService
    {
        IEnumerable<FavoriteGame> GetAll();
        FavoriteGame GetById(int id);
        Task<IEnumerable<Game>> GetFavoriteGameByUserIdAsync(string userId);
        void AddFavoriteGame(string userId, int gameId);
        void RemoveFavoriteGame(string userId,int gameId);
        bool IsFavorited(string userId, int gameId);
    }
}
