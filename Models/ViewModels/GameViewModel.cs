namespace GameForum.Models.ViewModels
{
    public class GameViewModel
    {
        public Game Game { get; set; }
        public IEnumerable<Review> GameReviews { get; set; }
        public IEnumerable<Discussion> GetDiscussions { get; set; }
    }
}
