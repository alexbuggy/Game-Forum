using GameForum.Models.Enums;
namespace GameForum.Models
{

    public class Vote
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public VoteType Type { get; set; }
    }
}
