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
        public async Task AddUser(PortStanleyUser newUser)
        {
            await _userService.AddUser(newUser);
        }

        [HttpGet]
        public async Task<PortStanleyUser> GetUser(string userName)
        {
            return await _userService.GetUser(userName);
        }
    }
}
