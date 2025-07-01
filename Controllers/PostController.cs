using GameForum.Models.Enums;
using GameForum.Models;
using GameForum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class PostController : Controller
{
    private readonly IVoteService _voteService;

    public PostController(IVoteService voteService)
    {
        _voteService = voteService;
    }

    [HttpPost]
    public IActionResult Vote([FromBody] VoteRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Or however you're getting the ID
        try
        {
            var success = _voteService.ToggleVote(request.PostId, userId, request.VoteType);
            return success ? Ok() : BadRequest("Vote failed.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public class VoteRequest
    {
        public int PostId { get; set; }
        public VoteType VoteType { get; set; }
    }
}
