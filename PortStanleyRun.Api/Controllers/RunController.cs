using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using PortStanleyRun.Api.Services.Interfaces;

namespace PortStanleyRun.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RunController : ControllerBase
    {
        private readonly IRunService _runService;

        public RunController(IRunService runService)
        {
            _runService = runService;
        }

        [HttpPost]
        [Authorize("create:runs")]
        public async Task AddRun(Models.PortStanleyRun newRun)
        {
            await _runService.AddRun(newRun);
        }

        [HttpGet]
        [Authorize("create:runs")]
        public async Task<Models.PortStanleyRun> GetRun(string objectId)
        {
            return await _runService.GetRun(new ObjectId(objectId));
        }

        [HttpDelete]
        [Authorize("delete:runs")]
        public async Task<bool> DeleteRun(string objectId)
        {
            return await _runService.DeleteRun(new ObjectId(objectId));
        }

        [HttpGet("GetAllRuns")]
        [Authorize("read:runs")]
        public async Task<List<Models.PortStanleyRun>> GetAllRuns()
        {
            return await _runService.GetAllRuns();
        }

        [HttpPost("AddRunner")]
        [Authorize("create:runs")]
        public async Task AddRunner(string runId, string runnerId)
        {
            await _runService.AddRunner(runId, runnerId);
        }
    }
}
