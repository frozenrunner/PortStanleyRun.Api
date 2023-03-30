using MongoDB.Bson;
using MongoDB.Driver;
using PortStanleyRun.Api.Models;
using PortStanleyRun.Api.Repositories.Interfaces;
using PortStanleyRun.Api.Services.Interfaces;

namespace PortStanleyRun.Api.Repositories
{
    public class RunRepository : IRunRepository
    {
        private readonly IMongoClient _client;
        private readonly IConfiguration _config;

        private readonly IMongoDatabase _portStandleyDb;
        private readonly IMongoCollection<Models.PortStanleyRun> _runs;

        public RunRepository(IConfiguration config,IMongoClient client)
        {
            _config = config;
            _client = client;

            _portStandleyDb = _client.GetDatabase(_config.GetValue<string>("Cosmos:DatabaseId"));
            _runs = _portStandleyDb.GetCollection<Models.PortStanleyRun>(_config.GetValue<string>("Cosmos:RunsContainer"));
        }

        public async Task<Models.PortStanleyRun> GetRun(string runId)
        {
            var run = await _runs.FindAsync(x => x.Id == new ObjectId(runId));
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

        public async Task<bool> AddRunner(string runId, string runnerId, string startingPoint)
        {
            var run = await GetRun(runId);
            var runners = run.Runners;
            runners.Add(new RunParticipant
            {
                RunnerId = new ObjectId(runnerId),
                StartingPoint = startingPoint
            });

            var filter = Builders<Models.PortStanleyRun>.Filter.Eq(r => r.Id, run.Id);
            var update = Builders<Models.PortStanleyRun>.Update.Set(r => r.Runners, run.Runners);
            var result = await _runs.UpdateOneAsync(filter, update);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteRun(string runId)
        {
            var run = await GetRun(runId);
            var filter = Builders<Models.PortStanleyRun>.Filter.Eq(r => r.Id, run.Id);

            var deleteResult = await _runs.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
