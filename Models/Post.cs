namespace GameForum.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public User Author { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int VoteNumber { get; set; }  // Single count representing net votes
        public int? GameId { get; set; }
        public Game Game { get; set; }
        public List<Vote> Votes { get; set; }  // Keep this collection for vote tracking
        public List<Reply> Replies { get; set; }
    }
}
