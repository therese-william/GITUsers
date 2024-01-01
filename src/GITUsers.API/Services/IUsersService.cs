using GITUsers.API.Models;

namespace GITUsers.API.Services
{
    public interface IUsersService
    {
        Task<User?> GetUser(string name);
        Task<IEnumerable<User>> GetUsersList(string[] userNames);
    }
}
