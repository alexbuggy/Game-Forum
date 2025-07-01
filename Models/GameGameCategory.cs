using GameForum.Models.Enums;

namespace GameForum.Models
{
    public class GameGameCategory
    {
        public int GameId { get; set; }
        public Game Game { get; set; }

        public GameCategory Category { get; set; }
    }

}
