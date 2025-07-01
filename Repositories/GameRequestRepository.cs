using GameForum.Data;
using GameForum.Models;
using GameForum.Models.Enums;
using GameForum.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameForum.Repositories
{
    public class GameRequestRepository : RepositoryBase<GameRequest>, IGameRequestRepository
    {
        public GameRequestRepository(GameForumContext gameforumContext) : base(gameforumContext)
        {

        }
        public GameRequest GetById(int id)
        {
            return GameForumContext.GameRequests
                .Include(r => r.GameCategories) // Include related data
                .FirstOrDefault(r => r.Id == id);
        }

        public void UpdateRequest(GameRequest request)
        {
            Update(request); // Uses base method which calls SaveChanges
        }

        public IEnumerable<GameRequest> GetPendingRequests()
        {
            return GameForumContext.GameRequests
                .Include(r => r.GameCategories)
                .Where(r => r.Status == RequestStatus.Pending)
                .ToList();
        }
    }
}
