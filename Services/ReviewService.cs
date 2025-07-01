using Microsoft.EntityFrameworkCore;
using GameForum.Models;
using GameForum.Repositories.Interfaces;
using GameForum.Services.Interfaces;
using GameForum.Repositories;

namespace GameForum.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IReplyService _replyService;
        
        public ReviewService(IRepositoryWrapper repository, IReplyService replyService)
        {
            _repo = repository;
            _replyService = replyService;
        }

        public void AddReview(Review review)
        {
            _repo.ReviewRepository.Create(review);
            _repo.Save();
        }

        public IEnumerable<Review> GetAll() => _repo.ReviewRepository.FindAll();

        public Review GetById(int id)
        {
            var review = _repo.ReviewRepository
              .FindByCondition(r => r.Id == id)
             .Include(r => r.Author)
             .Include(r => r.Replies)
                 .ThenInclude(reply => reply.Author)
             .FirstOrDefault();

            if (review != null)
            {
                foreach (var reply in review.Replies)
                {
                    _replyService.LoadRepliesRecursively(reply);
                }
            }

            return review;
        }

        public IEnumerable<Review> GetReviewsByGameId(int gameId)
        {
            return _repo.ReviewRepository
                .FindByCondition(r => r.GameId == gameId)
                .Include(r => r.Author).Include(re => re.Replies) 
                .ToList();
        }

        public IEnumerable<Review> GetReviewsByUserId(string userId)
        {
           return _repo.ReviewRepository.FindByCondition(x => x.AuthorId == userId).ToList();
        }

        public IEnumerable<Review> GetTop()
        {
            return _repo.ReviewRepository
             .FindAll()
              .Include(r => r.Game)
                 .Include(r => r.Author)
                 .OrderByDescending(x => x.VoteNumber)
                 .Take(3)
                 .ToList();
        }

        public Review AddOrUpdateReview(string userId, int gameId, string content, int rating)
        {
            var existingReview = _repo.ReviewRepository
                .FindByCondition(r => r.AuthorId == userId && r.GameId == gameId)
                .Include(r => r.Replies)
                .FirstOrDefault();

            var newReview = new Review();

            if (existingReview != null)
            {
                // Update existing review
                existingReview.Content = content;
                existingReview.Rating = rating;
                existingReview.CreatedAt = DateTime.UtcNow;
                existingReview.VoteNumber = 0;
                foreach (var reply in existingReview.Replies.ToList())
                {
                    _repo.ReplyRepository.Delete(reply);
                }
                _repo.ReviewRepository.Update(existingReview);
            }
            else
            {
                // Create new review
                newReview = new Review
                {
                    AuthorId = userId,
                    GameId = gameId,
                    Content = content,
                    Rating = rating,
                    CreatedAt = DateTime.UtcNow,
                    VoteNumber = 0
                };

                _repo.ReviewRepository.Create(newReview);
            }
            return existingReview ?? newReview;
        }


    }
}
