using GameForum.Models;
using GameForum.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameForum.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IReplyService _replyService;
        private readonly UserManager<User> _userManager;

        public ReviewsController(IReviewService reviewService, IReplyService replyService, UserManager<User> userManager)
        {
            _reviewService = reviewService;
            _replyService = replyService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var review = _reviewService.GetById(id);

            if (review == null || review.Author == null)
                return NotFound();

            return View(review);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddReply(int ReviewId, string Content, int? ParentReplyId, int RootId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId) || string.IsNullOrWhiteSpace(Content))
                return BadRequest();

            var reply = new Reply
            {
                AuthorId = userId,
                Content = Content,
                CreatedAt = DateTime.UtcNow,
                ParentPostId = ParentReplyId ?? ReviewId,
                GameId = null, 
                VoteNumber = 0,
                RootId = RootId
            };

            _replyService.AddReply(reply);
            return RedirectToAction("Details", new { id = RootId });
        }
    }
}
