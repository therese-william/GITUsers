using GITUsers.API.Extentions;
using GITUsers.API.Infrastructure;
using GITUsers.API.Models;

namespace GITUsers.API.Services
{
    public class UsersService : IUsersService
    {
        readonly ICacheProvider _cacheProvider;
        readonly IUserClient _userClient;
        readonly ILogger<UsersService> _logger;

        public UsersService(IUserClient userClient, ICacheProvider cacheProvider, ILogger<UsersService> logger)
        {
            _userClient = userClient;
            _cacheProvider = cacheProvider;
            _logger = logger;
        }

        public async Task<User?> GetUser(string name)
        {
            try
            {
                var user = _cacheProvider.GetFromCache<User>(name);
                if (user == null)
                {
                    user = await _userClient.GetUserByName(name);
                    if (user != null)
                    {
                        _cacheProvider.SetCache(name, user, 120);
                    }
                }
                return user;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving user [{name}]!");
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetUsersList(string[] userNames)
        {
            var users = new List<User>();
            foreach (var name in userNames.Distinct())
            { 
                var user = await GetUser(name);
                if (user != null)
                {
                    users.Add(user);
                }
            }
            var orderedUsers = users.OrderBy(u => u.Name).ToList();
            return orderedUsers;
        }
    }
}
