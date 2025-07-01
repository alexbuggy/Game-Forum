using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameForum.Data;
using GameForum.Models;
using GameForum.Services.Interfaces;
using GameForum.Models.Enums;
using GameForum.Models.ViewModels;
using GameForum.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GameForum.Controllers
{
    public class GamesController : Controller
    {
        private readonly GameForumContext _context;
        private readonly IGameService _gameService;
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;
        private readonly IDiscussionService _discussionService;
        private readonly IFavoriteGameService _favoriteGameService;
        private readonly UserManager<User> _userManager;


        public GamesController(IGameService gameService,IUserService userService,
                               IReviewService reviewService,IDiscussionService discussionService,
                               IFavoriteGameService favoriteGameService,UserManager<User> userManager)
        {
            _gameService = gameService;
            _userService = userService;
            _reviewService = reviewService;
            _discussionService = discussionService;
            _userManager = userManager;
            _favoriteGameService = favoriteGameService;
        }

        // GET: Games
        public IActionResult Index()
        {
            var allGames = _gameService.GetAllWithCategories(); // include GameCategories

            // Flatten to category → games
            var groupedByCategory = allGames
                .SelectMany(game => game.GameCategories.Select(gc => new { Category = gc.Category, Game = game }))
                .GroupBy(x => x.Category)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Game).Distinct().ToList());

            return View(groupedByCategory);
        }


        
        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var game = _gameService.GetByIdWithPosts(id.Value);
            if (game == null)
                return NotFound();

            var userId = _userManager.GetUserId(User);
            ViewData["IsFavorited"] = userId != null && _favoriteGameService.IsFavorited(userId, game.Id);

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            var model = new GameCreateViewModel
            {
                AvailableCategories = Enum.GetValues(typeof(GameCategory))
                    .Cast<GameCategory>()
                    .Select(c => new SelectListItem { Value = c.ToString(), Text = c.ToString() })
                    .ToList()
            };
            return View(model);
        }


        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableCategories = Enum.GetValues(typeof(GameCategory))
                    .Cast<GameCategory>()
                    .Select(c => new SelectListItem { Value = c.ToString(), Text = c.ToString() })
                    .ToList();
                return View(model);
            }

            var game = new Game
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                AverageRating = model.AverageRating,
                GameCategories = model.SelectedCategories.Select(cat => new GameGameCategory
                {
                    Category = cat
                }).ToList()
            };

            _gameService.AddGame(game);
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Authorize]
        public IActionResult AddFavoriteGame(int gameId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            if (_favoriteGameService.IsFavorited(userId, gameId))
            {
                _favoriteGameService.RemoveFavoriteGame(userId, gameId);
            }
            else
            {
                _favoriteGameService.AddFavoriteGame(userId, gameId);
            }

            return RedirectToAction("Details", new { id = gameId });
        }

        [HttpPost]
        [Authorize]
        public IActionResult SubmitReview(int gameId, string content, int rating)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            _reviewService.AddOrUpdateReview(userId, gameId, content, rating);
            _gameService.UpdateRating(rating , gameId);

            return RedirectToAction("Details", "Games", new { id = gameId });
        }

        [HttpPost]
        [Authorize]
        public IActionResult SubmitDiscussion(int gameId, string content)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var discussion = new Discussion
            {
                GameId = gameId,
                AuthorId = userId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            _discussionService.AddDiscussion(discussion);

            return RedirectToAction("Details", "Games", new { id = gameId });
        }




        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageUrl,AverageRating,Category")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
