using GITUsers.API.Models;
using GITUsers.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GITUsers.API.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUsersService _usersService;
        readonly ILogger<UsersController> _logger;

        public UsersController(IUsersService usersService, ILogger<UsersController> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        [HttpGet("/retrieveUsers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User[]))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveUsers([FromQuery]string[] userNames)
        {
            try
            {
                var users = await _usersService.GetUsersList(userNames);
                return Ok(users);
            }
            catch(Exception ex)
            {
                return Problem(detail: $"Internal error retrieving users details, please contact administrator!", statusCode: (int)HttpStatusCode.InternalServerError);
                _logger.LogError(ex, $"Error retieving details for users: {string.Join(",", userNames)}");
            }
        }
    }
}
