using MongoDB.Driver;
using PortStanleyRun.Api.Models;
using PortStanleyRun.Api.Services.Interfaces;

namespace PortStanleyRun.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoClient _client;
        private readonly IConfiguration _config;

        private IMongoDatabase _portStandleyDb;
        private IMongoCollection<PortStanleyUser> _users;

        public UserService(IConfiguration config, IMongoClient client)
	    {
            _config = config;
            _client = client;

            _portStandleyDb = _client.GetDatabase(_config.GetValue<string>("Cosmos:DatabaseId"));
            _users = _portStandleyDb.GetCollection<PortStanleyUser>(_config.GetValue<string>("Cosmos:UsersContainer"));
        }

        public async Task<PortStanleyUser> GetUser(string userName)
        {
            var user = await _users.FindAsync(x => string.Equals(x.UserName, userName, StringComparison.OrdinalIgnoreCase));
            return user.FirstOrDefault();
        }

        public async Task AddUser(PortStanleyUser user)
        {
            var existingUser = await GetUser(user.UserName);
            if (existingUser == null) {
                await _users.InsertOneAsync(user);
            }
            else
            {
                throw new BadHttpRequestException("User exists");
            }
        }
    }

}