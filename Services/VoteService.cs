using GameForum.Models.Enums;
using GameForum.Models;
using GameForum.Repositories;
using GameForum.Repositories.Interfaces;
using GameForum.Services.Interfaces;
namespace GameForum.Services
{
    public class VoteService : IVoteService
    {
        private readonly IRepositoryWrapper _repo;
        public VoteService(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public bool ToggleVote(int postId, string userId, VoteType voteType)
        {
            Console.WriteLine($"DEBUG: Received vote type: {voteType} with integer value: {(int)voteType}");

            // Find the post
            var post = FindPostById(postId);
            if (post == null)
            {
                throw new ArgumentException("Post not found.");
            }

            // Check if the user has already voted on this post
            var existingVote = _repo.VoteRepository.FindByCondition(v => v.PostId == postId && v.UserId == userId).FirstOrDefault();

            if (existingVote != null)
            {
                Console.WriteLine($"DEBUG: Existing vote found with type: {existingVote.Type} and integer value: {(int)existingVote.Type}");
            }

            try
            {
                // Handle the vote (using the enum integer values directly)
                if (existingVote != null)
                {
                    // If same vote type, remove it (toggle off)
                    if (existingVote.Type == voteType)
                    {
                        // Remove the vote's value from the total
                        post.VoteNumber -= (int)existingVote.Type;
                        _repo.VoteRepository.Delete(existingVote);
                    }
                    // If different vote type, update it
                    else
                    {
                        // Remove old vote value and add new vote value
                        post.VoteNumber -= (int)existingVote.Type;
                        post.VoteNumber += (int)voteType;

                        existingVote.Type = voteType;
                        _repo.VoteRepository.Update(existingVote);
                    }
                }
                else
                {
                    // Create new vote and add its value
                    post.VoteNumber += (int)voteType;

                    var newVote = new Vote
                    {
                        PostId = postId,
                        UserId = userId,
                        Type = voteType,
                    };
                    _repo.VoteRepository.Create(newVote);
                }

                // Update the post
                if (post is Review review)
                {
                    _repo.ReviewRepository.Update(review);
                }
                else if (post is Discussion discussion)
                {
                    _repo.DiscussionRepository.Update(discussion);
                }
                else if(post is Reply reply)
                {
                    _repo.ReplyRepository.Update(reply);
                }
                    //else if (post is Reply reply)
                    //{
                    //    _repo.ReplyRepository.Update(reply);
                    //}

                    // Save all changes
                    _repo.Save();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error toggling vote: {ex.Message}");
                return false;
            }
        }



        private Post FindPostById(int postId)
        {
            // Search for the post in discussions
            var discussion = _repo.DiscussionRepository.FindByCondition(d => d.Id == postId).FirstOrDefault();
            if (discussion != null)
            {
                return discussion;
            }

            // Search for the post in reviews
            var review = _repo.ReviewRepository.FindByCondition(r => r.Id == postId).FirstOrDefault();
            if (review != null)
            {
                return review;
            }

            var reply = _repo.ReplyRepository.FindByCondition(rp => rp.Id == postId).FirstOrDefault();
            if (reply != null)
            {
                return reply;
            }

            return null; // Return null if no post is found in either
        }


    }
}
