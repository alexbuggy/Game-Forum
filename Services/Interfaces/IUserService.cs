using GameForum.Models;

namespace GameForum.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string id);
        Task UpdateProfileAsync(string id, string newUsername, string aboutMe, string newImg);

    }

}
