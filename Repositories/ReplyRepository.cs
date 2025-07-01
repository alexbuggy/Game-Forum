using GameForum.Data;
using GameForum.Models;
using GameForum.Repositories.Interfaces;

namespace GameForum.Repositories
{
    public class ReplyRepository:RepositoryBase<Reply>, IReplyRepository
    {
        public ReplyRepository(GameForumContext gameforumContext) : base(gameforumContext)
        {

        }

    }
}
