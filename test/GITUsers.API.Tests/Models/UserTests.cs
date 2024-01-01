using GITUsers.API.Models;

namespace GITUsers.API.Tests.Models
{
    public class UserTests
    {
        [Fact]
        public void UserAverageFollowers()
        {
            var user = new User
            {
                Company = "Company",
                Followers = 100,
                Login = "Login",
                Name = "Name",
                PublicRepos = 5
            };
            Assert.Equal(20,user.AverageFollowers);
        }

    }
}
