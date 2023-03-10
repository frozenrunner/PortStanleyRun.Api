using MongoDB.Driver;
using PortStanleyRun.Api.Models;
using PortStanleyRun.Api.Services.Interfaces;
using System.Security.Authentication;

namespace PortStanleyRun.Api.Services
{
    public class MongoService : IMongoService
    {
        private readonly MongoClient _client;
        private readonly IConfiguration _config;

        private IMongoDatabase _portStandleyDb;
        private IMongoCollection<PortStanleyUser> _users;

        public MongoService(IConfiguration config)
	    {
            _config = config;

            var mongoSettings = MongoClientSettings.FromUrl(new MongoUrl(string.Format(_config.GetConnectionString("MongoDb"), _config.GetValue<string>("PrimaryPassword"))));
            mongoSettings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
            mongoSettings.ConnectTimeout = TimeSpan.FromSeconds(60);
            
            _client = new MongoClient(mongoSettings);
        }
        
        public async Task AddUser(PortStanleyUser user)
        {
            CreateDatabaseAsync();
            CreateContainerAsync();
            user.Id = Guid.NewGuid().ToString();
            try
            {
                await _users.InsertOneAsync(user);
            } catch (Exception ex)
            {

            }
            
        }

        // <CreateDatabaseAsync>
        /// <summary>
        /// Create the database if it does not exist
        /// </summary>
        private void CreateDatabaseAsync()
        {
            // Create a new database
            _portStandleyDb = _client.GetDatabase(_config.GetValue<string>("Cosmos:DatabaseId"));
        }

        // <CreateContainerAsync>
        /// <summary>
        /// Create the container if it does not exist. 
        /// </summary>
        /// <returns></returns>
        private void CreateContainerAsync()
        {
            // Create a new container
            _users = _portStandleyDb.GetCollection<PortStanleyUser>(_config.GetValue<string>("Cosmos:ContainerId"));
        }
    }

}