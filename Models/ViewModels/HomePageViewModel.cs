namespace GameForum.Models.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<Game> Top3Games { get; set; }
        public IEnumerable<Review> Top3Reviews { get; set; }
    }
}
