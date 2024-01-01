using FluentAssertions;
using GITUsers.API.Models;
using GITUsers.API.Infrastructure;
using GITUsers.API.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace GITUsers.API.Tests.Services
{
    public class UsersServiceTests
    {
        protected string _str => It.IsAny<string>();

        [Fact]
        public async Task GetUserFromCache()
        {
            var mockUserClient = new Mock<IUserClient>();
            var mockCacheProvider = new Mock<ICacheProvider>();
            var mockLogger = Mock.Of<ILogger<UsersService>>();

            var expectedUser = new User
            {
                Company = "company",
                Followers = 100,
                Login = "login",
                Name = "name",
                PublicRepos = 5
            };
            mockCacheProvider.Setup(x => x.GetFromCache<User>(_str)).Returns(expectedUser);
            var usersService = new UsersService(mockUserClient.Object, mockCacheProvider.Object, mockLogger);
            var user = await usersService.GetUser("name");
            expectedUser.Should().BeEquivalentTo(user);
            mockCacheProvider.Verify(x => x.GetFromCache<User>(_str), Times.Once);
            mockUserClient.Verify(x => x.GetUserByName(_str), Times.Never);
        }

        [Fact]
        public async Task GetUserFromClient()
        {
            var mockLogger = Mock.Of<ILogger<UsersService>>();
            var mockUserClient = new Mock<IUserClient>();
            var mockCacheProvider = new Mock<ICacheProvider>();
            var expectedUser = new User
            {
                Company = "company",
                Followers = 100,
                Login = "login",
                Name = "name",
                PublicRepos = 5
            };
            mockUserClient.Setup(x => x.GetUserByName(_str)).ReturnsAsync(expectedUser);
            var usersService = new UsersService(mockUserClient.Object, mockCacheProvider.Object, mockLogger);
            var user = await usersService.GetUser("name");
            expectedUser.Should().BeEquivalentTo(user);
            mockCacheProvider.Verify(x => x.GetFromCache<User>(_str), Times.Once);
            mockUserClient.Verify(x => x.GetUserByName(_str), Times.AtLeastOnce);
        }
    }
}
