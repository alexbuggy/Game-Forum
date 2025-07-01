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
using Microsoft.AspNetCore.Identity;
using GameForum.Models.Enums;
using GameForum.Models.ViewModels;
using GameForum.Services;
using Microsoft.AspNetCore.Authorization;

namespace GameForum.Controllers
{
    public class GameRequestsController : Controller
    {
        private readonly GameForumContext _context;
        private readonly IGameRequestService _gameRequestService;
        private readonly IGameService _gameService;
        private readonly UserManager<User> _userManager;

        public GameRequestsController(GameForumContext context,IGameRequestService gameRequestService, UserManager<User> userManager, IGameService gameService)
        {
            _context = context;
            _gameRequestService = gameRequestService;
            _userManager = userManager;
            _gameService = gameService;
        }

        // GET: GameRequests
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var requests = _gameRequestService.GetAllPendingRequests();
            return View(requests);
        }

        // GET: GameRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameRequest = await _context.GameRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameRequest == null)
            {
                return NotFound();
            }

            return View(gameRequest);
        }

        // GET: GameRequests/Create
        public IActionResult Create()
        {
            var model = new GameRequestViewModel
            {
                AvailableCategories = Enum.GetValues(typeof(GameCategory))
                     .Cast<GameCategory>()
                    .Select(c => new SelectListItem
                         {
                          Text = c.ToString(),
                            Value = c.ToString()
                         }).ToList()
            };

            return View(model);
        }

        // POST: GameRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GameRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableCategories = Enum.GetValues(typeof(GameCategory))
                    .Cast<GameCategory>()
                    .Select(c => new SelectListItem
                    {
                        Text = c.ToString(),
                        Value = c.ToString()
                    }).ToList();
                return View(model);
            }

            var userId = _userManager.GetUserId(User);
            var request = new GameRequest
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                RequestedByUserId = userId,
                Status = RequestStatus.Pending,
                RequestedAt = DateTime.UtcNow,
                GameCategories = model.SelectedCategories.Select(cat => new GameRequestCategory
                {
                    Category = cat
                }).ToList()
            };

            _gameRequestService.AddGame(request);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        // GET: GameRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameRequest = await _context.GameRequests.FindAsync(id);
            if (gameRequest == null)
            {
                return NotFound();
            }
            return View(gameRequest);
        }

        // POST: GameRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageUrl,RequestedByUserId,Status,RequestedAt")] GameRequest gameRequest)
        {
            if (id != gameRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameRequestExists(gameRequest.Id))
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
            return View(gameRequest);
        }

        // GET: GameRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameRequest = await _context.GameRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameRequest == null)
            {
                return NotFound();
            }

            return View(gameRequest);
        }

        // POST: GameRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameRequest = await _context.GameRequests.FindAsync(id);
            if (gameRequest != null)
            {
                _context.GameRequests.Remove(gameRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameRequestExists(int id)
        {
            return _context.GameRequests.Any(e => e.Id == id);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult HandleRequest(int id, string actionType)
        {
            var request = _gameRequestService.GetById(id);
            if (request == null || request.Status != RequestStatus.Pending)
            {
                return NotFound();
            }

            if (actionType == "accept")
            {
                // Create new Game
                var newGame = new Game
                {
                    Title = request.Title,
                    Description = request.Description,
                    ImageUrl = request.ImageUrl,
                    GameCategories = request.GameCategories.Select(gc => new GameGameCategory
                    {
                        Category = gc.Category
                    }).ToList()
                };

                _gameService.AddGame(newGame);

                // Update request status
                request.Status = RequestStatus.Approved;
                _gameRequestService.Update(request);
            }
            else if (actionType == "reject")
            {
                request.Status = RequestStatus.Rejected;
                _gameRequestService.Update(request);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
