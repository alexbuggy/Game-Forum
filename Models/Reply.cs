namespace GameForum.Models
{
    public class Reply:Post
    {
        public int? ParentPostId { get; set; } // Refers to the post (Discussion/Review/Reply) being replied to
        public Post ParentPost { get; set; }

        public int RootId { get; set; }
    }
}
