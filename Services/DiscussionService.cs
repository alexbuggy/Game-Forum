using GameForum.Models;
using GameForum.Repositories.Interfaces;
using GameForum.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace GameForum.Services
{
    public class DiscussionService : IDiscussionService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IReplyService _replyService;

        public DiscussionService(IRepositoryWrapper repository, IReplyService replyService  )
        {
            _repo = repository;
            _replyService = replyService;
        }

        public void AddDiscussion(Discussion discussion)
        {
            _repo.DiscussionRepository.Create(discussion);
            _repo.Save();
        }

        public IEnumerable<Discussion> GetAll()
        {
            return _repo.DiscussionRepository
                .FindAll()
                .Include(d => d.Author)
                .ToList();
        }

        public Discussion GetById(int id)
        {
            var discussion = _repo.DiscussionRepository
                .FindByCondition(d => d.Id == id)
                .Include(d => d.Author)
                .Include(d => d.Replies)
                .ThenInclude(r => r.Author)
                .FirstOrDefault();

            if (discussion != null)
            {
                foreach (var reply in discussion.Replies)
                {
                    _replyService.LoadRepliesRecursively(reply);
                }
            }
            return discussion;
        }

        public IEnumerable<Discussion> GetDiscussionByGameId(int gameId)
        {
            return _repo.DiscussionRepository
                .FindByCondition(d => d.GameId == gameId)
                 .Include(d => d.Author).Include(re => re.Replies)
                .ToList();
        }

        public IEnumerable<Discussion> GetDiscussionByUserId(string userId)
        {
            return _repo.DiscussionRepository
                .FindByCondition(d => d.AuthorId == userId)
                .Include(d => d.Game)
                .ToList();
        }
    }
}
