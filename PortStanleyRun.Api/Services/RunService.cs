using MongoDB.Driver;
using PortStanleyRun.Api.Repositories;
using PortStanleyRun.Api.Repositories.Interfaces;
using PortStanleyRun.Api.Services.Interfaces;

namespace PortStanleyRun.Api.Services
{
    public class RunService : IRunService
    {
        private readonly IRunRepository _runRepository;
        public RunService(IRunRepository runRepository)
        {
            _runRepository = runRepository;
        }

        public async Task<Models.PortStanleyRun> GetRun(string runId)
        {
            var result = await _runRepository.GetRun(runId);
            return result;
        }

        public async Task<List<Models.PortStanleyRun>> GetAllRuns()
        {
            var result = await _runRepository.GetAllRuns();
            return result;
        }

        public async Task AddRun(Models.PortStanleyRun run)
        {
            await _runRepository.AddRun(run);
        }

        public async Task<bool> AddRunner(string runId, string runnerId, string startingPoint)
        {
            var result = await _runRepository.AddRunner(runId, runnerId, startingPoint);

            return result;
        }

        public async Task<bool> DeleteRun(string runId)
        {
            var result = await _runRepository.DeleteRun(runId);
            return result;
        }
    }
}
