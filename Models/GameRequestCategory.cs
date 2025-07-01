using GameForum.Models.Enums;

namespace GameForum.Models
{
    public class GameRequestCategory
    {
        public int GameRequestId { get; set; }
        public GameRequest GameRequest { get; set; }

        public GameCategory Category { get; set; }
    }
}
