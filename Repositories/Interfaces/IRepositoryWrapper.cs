namespace GameForum.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IDiscussionRepository DiscussionRepository { get; }
        IGameRepository GameRepository { get; }
        IGameRequestRepository GameRequestRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IUserRepository UserRepository { get; }
        IReplyRepository ReplyRepository { get; }
        IVoteRepository VoteRepository { get; }
        IFavoriteGameRepository FavoriteGameRepository { get; }
        void Save();
    }
}
