using GITUsers.API.Extentions;
using GITUsers.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace GITUsers.API.Tests.Controllers
{
    public class UsersControllerTests
    {
        private HttpClient _httpClient;

        public UsersControllerTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task GetUsers_Get_ReturnUserList()
        {
            var response = await _httpClient.GetAsync("/retrieveUsers?userNames=mojombo&userNames=defunkt");
            response.EnsureSuccessStatusCode();
            var users = await response.Deserialize<List<User>>();
            Assert.Equal(2, users?.Count());
        }
    }
}
