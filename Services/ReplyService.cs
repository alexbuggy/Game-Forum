using GameForum.Models;
using GameForum.Repositories.Interfaces;
using GameForum.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameForum.Services
{
    public class ReplyService:IReplyService
    {
        private readonly IRepositoryWrapper _repo;

        public ReplyService(IRepositoryWrapper repository)
        {
            _repo = repository;
        }

        public void AddReply(Reply reply)
        {
            _repo.ReplyRepository.Create(reply);
            _repo.Save();
        }

        public void LoadRepliesRecursively(Reply reply)
        {
            var childReplies = _repo.ReplyRepository
                .FindByCondition(r => r.ParentPostId == reply.Id)
                .Include(r => r.Author)
                .ToList();

            reply.Replies = childReplies;

            foreach (var child in childReplies)
            {
                LoadRepliesRecursively(child); // recursive step
            }
        }
    }
}
