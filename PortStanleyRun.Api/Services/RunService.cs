using MongoDB.Bson;
using MongoDB.Driver;
using PortStanleyRun.Api.Services.Interfaces;

namespace PortStanleyRun.Api.Services
{
    public class RunService : IRunService
    {
        private readonly IMongoClient _client;
        private readonly IConfiguration _config;

        private readonly IMongoDatabase _portStandleyDb;
        private readonly IMongoCollection<Models.PortStanleyRun> _runs;

        public RunService(IConfiguration config,IMongoClient client)
        {
            _config = config;
            _client = client;

            _portStandleyDb = _client.GetDatabase(_config.GetValue<string>("Cosmos:DatabaseId"));
            _runs = _portStandleyDb.GetCollection<Models.PortStanleyRun>(_config.GetValue<string>("Cosmos:RunsContainer"));
        }

        public async Task<Models.PortStanleyRun> GetRun(string runId)
        {
            var run = await _runs.FindAsync(x => x._id == new ObjectId(runId));
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

        public async Task<UpdateResult> AddRunner(string runId, string runnerId)
        {
            var run = await GetRun(runId);
            var runners = run.Runners;
            runners.Add(new ObjectId(runnerId));

            var filter = Builders<Models.PortStanleyRun>.Filter.Eq(r => r._id, run._id);
            var update = Builders<Models.PortStanleyRun>.Update.Set(r => r.Runners, run.Runners);
            return await _runs.UpdateOneAsync(filter, update);
        }

        public async Task<bool> DeleteRun(string runId)
        {
            var run = await GetRun(runId);
            var filter = Builders<Models.PortStanleyRun>.Filter.Eq(r => r._id, run._id);

            var deleteResult = await _runs.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged ? deleteResult.DeletedCount > 0 : false;
        }
    }
}
