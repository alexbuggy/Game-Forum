using GameForum.Models;
using GameForum.Repositories.Interfaces;
using GameForum.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace GameForum.Services
{
    public class GameRequestService : IGameRequestService
    {
        private readonly IRepositoryWrapper _repo;

        public GameRequestService(IRepositoryWrapper repository)
        {
            _repo = repository;
        }

        public void AddGame(GameRequest gameRequest) => _repo.GameRequestRepository.Create(gameRequest);

        public IEnumerable<GameRequest> GetAll()
        {
            return _repo.GameRequestRepository
            .FindAll()
             .Include(r => r.RequestedByUser)
                .Include(r => r.GameCategories)
                 .ToList();
        }

        public IEnumerable<GameRequest> GetAllPendingRequests()
        {
            return _repo.GameRequestRepository.GetPendingRequests();
        }

        public GameRequest GetById(int id)
        {
            return _repo.GameRequestRepository.GetById(id);
        }

        public void Update(GameRequest request)
        {
            _repo.GameRequestRepository.UpdateRequest(request);
        }



    }
}
