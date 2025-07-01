using GameForum.Models;

namespace GameForum.Repositories.Interfaces
{
    public interface IGameRequestRepository : IRepositoryBase<GameRequest>
    {
        GameRequest GetById(int id);
        void UpdateRequest(GameRequest request);
        IEnumerable<GameRequest> GetPendingRequests();
    }
}
