using Microsoft.AspNetCore.Identity;

namespace GameForum.Models
{
    public class User:IdentityUser
    {
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Reply> Replies { get; set; }
        public ICollection<Discussion> DiscussionPosts { get; set; }
        public ICollection<FavoriteGame> FavoriteGames { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public ICollection<GameRequest> GameRequests { get; set; }
        public string ProfileImg { get; set; } = "https://static.vecteezy.com/system/resources/previews/009/292/244/non_2x/default-avatar-icon-of-social-media-user-vector.jpg";
        public string AboutMe { get; set; } = "Just another mysterious gamer lurking in the shadows...";
    }
}
