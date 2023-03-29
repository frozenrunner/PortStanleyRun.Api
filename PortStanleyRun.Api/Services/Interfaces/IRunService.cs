using MongoDB.Bson;
using MongoDB.Driver;

namespace PortStanleyRun.Api.Services.Interfaces
{
    public interface IRunService
    {
        /// <summary>
        /// Get a Port Stanley Run
        /// </summary>
        /// <param name="runId">MongoDb ObjectId for a run</param>
        /// <returns>A single Port Stanley Run</returns>
        Task<Models.PortStanleyRun> GetRun(string runId);
        
        /// <summary>
        /// Get all Port Stanley Runs
        /// </summary>
        /// <returns>All Port Stanley Runs</returns>
        Task<List<Models.PortStanleyRun>> GetAllRuns();

        /// <summary>
        /// Create a new Port Stanely Run
        /// </summary>
        /// <param name="run">Details of the new Port Stanley Run</param>
        Task AddRun(Models.PortStanleyRun run);

        /// <summary>
        /// Add a runner to a Port Stanley Run
        /// </summary>
        /// <param name="runId">ObjectId string for a run</param>
        /// <param name="runnerId">ObjectId string for a runner</param>
        /// <returns>Result of the update</returns>
        Task<UpdateResult> AddRunner(string runId, string runnerId);

        /// <summary>
        /// Delete a Port Stanley Run
        /// </summary>
        /// <param name="runId">ObjectId of the run to be deleted</param>
        /// <returns>true if the run was deleted, false otherwise</returns>
        Task<bool> DeleteRun(string runId);
    }
}
