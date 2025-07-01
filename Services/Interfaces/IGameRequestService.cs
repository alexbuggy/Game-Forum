using GameForum.Models;

namespace GameForum.Services.Interfaces
{
    public interface IGameRequestService

    {
        void AddGame(GameRequest gameRequest);
        IEnumerable<GameRequest> GetAll();
        GameRequest GetById(int id);
        void Update(GameRequest request);
        IEnumerable<GameRequest> GetAllPendingRequests();
    }
}
