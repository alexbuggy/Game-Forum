using GameForum.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace GameForum.Models
{
    public class GameRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<GameRequestCategory> GameCategories { get; set; } = new List<GameRequestCategory>();

        public User RequestedByUser { get; set; }

        public string RequestedByUserId { get; set; } 

        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow; 
    }
}
