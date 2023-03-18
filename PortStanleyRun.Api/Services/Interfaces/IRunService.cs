using MongoDB.Bson;

namespace PortStanleyRun.Api.Services.Interfaces
{
    public interface IRunService
    {

        Task<Models.PortStanleyRun> GetRun(ObjectId objectId);
        Task<List<Models.PortStanleyRun>> GetAllRuns();
        Task AddRun(Models.PortStanleyRun run);
    }
}
