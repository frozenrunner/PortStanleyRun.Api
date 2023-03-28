using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortStanleyRun.Api.Models;
using PortStanleyRun.Api.Services.Interfaces;

namespace PortStanleyRun.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService mongoService)
        {
            _userService = mongoService;
        }

        [HttpPut]
        [Authorize("create:users")]
        public async Task AddUser(PortStanleyUser newUser)
        {
            await _userService.AddUser(newUser);
        }

        [HttpGet]
        [Authorize("read:users")]
        public async Task<PortStanleyUser> GetUser(string userName)
        {
            return await _userService.GetUser(userName);
        }
    }
}
