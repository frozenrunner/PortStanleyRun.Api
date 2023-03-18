using MongoDB.Bson;
using MongoDB.Driver;
using PortStanleyRun.Api.Models;
using PortStanleyRun.Api.Services.Interfaces;
using Models = PortStanleyRun.Api.Models;

namespace PortStanleyRun.Api.Services
{
    public class RunService : IRunService
    {
        private readonly IMongoClient _client;
        private readonly IConfiguration _config;

        private IMongoDatabase _portStandleyDb;
        private IMongoCollection<Models.PortStanleyRun> _runs;

        public RunService(IConfiguration config,IMongoClient client)
        {
            _config = config;
            _client = client;

            _portStandleyDb = _client.GetDatabase(_config.GetValue<string>("Cosmos:DatabaseId"));
            _runs = _portStandleyDb.GetCollection<Models.PortStanleyRun>(_config.GetValue<string>("Cosmos:RunsContainer"));
        }

        public async Task<Models.PortStanleyRun> GetRun(ObjectId objectId)
        {
            var run = await _runs.FindAsync(x => x._id == objectId);
            return run.FirstOrDefault();
        }

        public async Task<List<Models.PortStanleyRun>> GetAllRuns()
        {
            var runs = await _runs.FindAsync(Builders<Models.PortStanleyRun>.Filter.Empty);
            return await runs.ToListAsync();
        }

        public async Task AddRun(Models.PortStanleyRun run)
        {
            await _runs.InsertOneAsync(run);
        }
    }
}
