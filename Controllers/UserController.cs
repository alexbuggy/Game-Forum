using GameForum.Models;
using GameForum.Services;
using GameForum.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IFavoriteGameService _favoriteService;
    private readonly IReviewService _reviewService;
    private readonly IDiscussionService _discussionService;

    public UserController(IUserService userService, IFavoriteGameService favoriteService,
                          IReviewService reviewService, IDiscussionService discussionService)
    {
        _userService = userService;
        _favoriteService = favoriteService;
        _reviewService = reviewService;
        _discussionService = discussionService;
    }

    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userService.GetUserByIdAsync(userId);

        ViewData["IsPublicProfile"] = false;


        return View(user);
    }

    [Route("User/PublicProfile/{userId}")]
    public async Task<IActionResult> PublicProfile(string userId)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userService.GetUserByIdAsync(userId);
        if (currentUserId == userId)
        {
            ViewData["IsPublicProfile"] = false;
        }
        else
        {
            ViewData["IsPublicProfile"] = true;
        }
        return View("Profile", user); ;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateProfile(User updatedUser)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _userService.UpdateProfileAsync(userId, updatedUser.UserName, updatedUser.AboutMe, updatedUser.ProfileImg);
        return RedirectToAction("Profile");
    }
}
