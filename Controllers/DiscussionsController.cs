using GameForum.Models;
using GameForum.Services;
using GameForum.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameForum.Controllers
{
    public class DiscussionsController : Controller
    {
        private readonly IDiscussionService _discussionService;
        private readonly IReplyService _replyService;
        private readonly UserManager<User> _userManager;


        public DiscussionsController(IDiscussionService discussionService, IReplyService replyService, UserManager<User> userManager)
        {
            _discussionService = discussionService;
            _replyService = replyService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var discussion = _discussionService.GetById(id);

            if (discussion == null || discussion.Author == null)
                return NotFound();

            return View(discussion);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddReply(int ReviewId, string Content, int? ParentReplyId, int RootId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId) || string.IsNullOrWhiteSpace(Content))
                return BadRequest();

            // Validate RootId
            if (RootId == 0)
            {
                // If RootId is 0, it might be a top-level reply, so set RootId to ReviewId
                RootId = ReviewId;
            }

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
