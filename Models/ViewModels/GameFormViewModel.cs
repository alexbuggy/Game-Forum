using GameForum.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameForum.Models.ViewModels
{
    public class GameCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double AverageRating { get; set; }
        public List<GameCategory> SelectedCategories { get; set; } = new();

        // All categories for the view
        public List<SelectListItem> AvailableCategories { get; set; } = new();
    }


}
