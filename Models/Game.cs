using Microsoft.AspNetCore.Mvc.ViewEngines;
using GameForum.Models.Enums;

namespace GameForum.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double AverageRating { get; set; }
        public ICollection<GameGameCategory> GameCategories { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Discussion> Discussions { get; set; }
        public ICollection<FavoriteGame> FavoritedBy { get; set; }
    }
}
