using GITUsers.API.Extentions;
using GITUsers.API.Models;

namespace GITUsers.API.Infrastructure
{
    public class UserClient : IUserClient
    {
        readonly HttpClient _client;
        readonly ILogger<UserClient> _logger;

        public UserClient(HttpClient client, ILogger<UserClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<User?> GetUserByName(string name)
        {
            try
            {
                var userResponse = await _client.GetAsync($"{name}");
                userResponse.EnsureSuccessStatusCode();
                var user = await userResponse.Deserialize<User>();
                return user;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving user [{name}] from client!");
                return null;
            }
        }
    }
}
