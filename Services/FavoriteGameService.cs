using GameForum.Models;
using GameForum.Repositories.Interfaces;
using GameForum.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameForum.Services
{
    public class FavoriteGameService : IFavoriteGameService
    {

        private readonly IRepositoryWrapper _repo;

        public FavoriteGameService(IRepositoryWrapper repository)
        {
            _repo = repository;
        }

        public void AddFavoriteGame(string userId, int gameId)
        {
            if (IsFavorited(userId, gameId)) return;

            var favorite = new FavoriteGame
            {
                UserId = userId,
                GameId = gameId
            };

            _repo.FavoriteGameRepository.Create(favorite);
            _repo.Save();
        }

        public IEnumerable<FavoriteGame> GetAll()
        {
            return _repo.FavoriteGameRepository.FindAll().ToList();
        }

        public FavoriteGame GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Game>> GetFavoriteGameByUserIdAsync(string userId)
        {
            return await _repo.FavoriteGameRepository
                .FindByCondition(f => f.UserId == userId)
                .Include(f => f.Game) 
                .Select(f => f.Game)
                .ToListAsync();
        }

        public bool IsFavorited(string userId, int gameId)
        {
            return _repo.FavoriteGameRepository
                .FindByCondition(f => f.UserId == userId && f.GameId == gameId)
                .Any();
        }

        public void RemoveFavoriteGame(string userId, int gameId)
        {
            var favorite = _repo.FavoriteGameRepository
                .FindByCondition(f => f.UserId == userId && f.GameId == gameId)
                .FirstOrDefault();

            if (favorite != null)
            {
                _repo.FavoriteGameRepository.Delete(favorite);
                _repo.Save();
            }
        }
    }
}
