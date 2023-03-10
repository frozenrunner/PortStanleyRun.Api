using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using PortStanleyRun.Api.Models;
using PortStanleyRun.Api.Services;
using PortStanleyRun.Api.Services.Interfaces;

namespace PortStanleyRun.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IMongoService _cosmosService;

        public UserController(IMongoService cosmosService)
        {
            _cosmosService = cosmosService;
        }

        [HttpPost(Name = "CreateUser")]
        public async Task CreateUser(PortStanleyUser newUser)
        {
            await _cosmosService.AddUser(newUser);
        }
    }
}
