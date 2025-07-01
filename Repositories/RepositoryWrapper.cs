using GameForum.Data;
using GameForum.Repositories.Interfaces;

namespace GameForum.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private GameForumContext _gameforumContext;

        private IDiscussionRepository? _DiscussionRepository;

        public IDiscussionRepository DiscussionRepository
        {
            get
            {
                if (_DiscussionRepository == null)
                {
                    _DiscussionRepository = new DiscussionRepository(_gameforumContext);
                }

                return _DiscussionRepository;
            }
        }


        private IGameRepository? _GameRepository;

        public IGameRepository GameRepository
        {
            get
            {
                if (_GameRepository == null)
                {
                    _GameRepository = new GameRepository(_gameforumContext);
                }

                return _GameRepository;
            }
        }



        private IGameRequestRepository? _GameRequestRepository;

        public IGameRequestRepository GameRequestRepository
        {
            get
            {
                if (_GameRequestRepository == null)
                {
                    _GameRequestRepository = new GameRequestRepository(_gameforumContext);
                }

                return _GameRequestRepository;
            }
        }
        

        private IReviewRepository? _ReviewRepository;

        public IReviewRepository ReviewRepository
        {
            get
            {
                if (_ReviewRepository == null)
                {
                    _ReviewRepository = new ReviewRepository(_gameforumContext);
                }

                return _ReviewRepository;
            }
        }



        private IUserRepository? _UserRepository;

        public IUserRepository UserRepository
        {
            get
            {
                if (_UserRepository == null)
                {
                    _UserRepository = new UserRepository(_gameforumContext);
                }

                return _UserRepository;
            }
        }


        private IReplyRepository? _ReplyRepository;

        public IReplyRepository ReplyRepository
        {
            get
            {
                if (_ReplyRepository == null)
                {
                    _ReplyRepository = new ReplyRepository(_gameforumContext);
                }

                return _ReplyRepository;
            }
        }

        private IVoteRepository? _VoteRepository;

        public IVoteRepository VoteRepository
        {
            get
            {
                if (_VoteRepository == null)
                {
                    _VoteRepository = new VoteRepository(_gameforumContext);
                }

                return _VoteRepository;
            }
        }

        private IFavoriteGameRepository? _FavoriteGameRepository;

        public IFavoriteGameRepository FavoriteGameRepository
        {
            get
            {
                if (_FavoriteGameRepository == null)
                {
                    _FavoriteGameRepository = new FavoriteGameRepository(_gameforumContext);
                }

                return _FavoriteGameRepository;
            }
        }


        public RepositoryWrapper(GameForumContext gameforumContext)
        {
            _gameforumContext = gameforumContext;
        }

        public void Save()
        {
            _gameforumContext.SaveChanges();
        }
    }
}
