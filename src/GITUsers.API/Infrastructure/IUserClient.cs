using GITUsers.API.Models;

namespace GITUsers.API.Infrastructure
{
    public interface IUserClient
    {
        Task<User?> GetUserByName(string name);
    }
}
