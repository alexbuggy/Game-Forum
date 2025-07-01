using System.Diagnostics;
using System.Threading.Tasks;
using GameForum.Models;
using GameForum.Models.ViewModels;
using GameForum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGameService _gameService;
        private readonly IReviewService _reviewService;



        public HomeController(ILogger<HomeController> logger,IGameService gameService, IReviewService reviewService)
        {
            _logger = logger;
            _gameService = gameService;
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            var topGames = _gameService.GetTop();
            var topReviews = _reviewService.GetTop();

            var viewModel = new HomePageViewModel
            {
                Top3Games = topGames,
                Top3Reviews = topReviews
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
