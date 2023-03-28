using MongoDB.Bson;
using MongoDB.Driver;

namespace PortStanleyRun.Api.Services.Interfaces
{
    public interface IRunService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        Task<Models.PortStanleyRun> GetRun(ObjectId objectId);
        Task<List<Models.PortStanleyRun>> GetAllRuns();
        Task AddRun(Models.PortStanleyRun run);
        Task<UpdateResult> AddRunner(string runId, string runnerId);
        Task<bool> DeleteRun(ObjectId objectId);
    }
}
