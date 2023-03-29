using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<Models.PortStanleyRun> GetRun(string runId)
        {
            return await _runService.GetRun(runId);
        }

        [HttpGet("GetAllRuns")]
#if !DEBUG
        [Authorize("read:runs")]
#endif
#if DEBUG
        [AllowAnonymous]
#endif
        public async Task<List<Models.PortStanleyRun>> GetAllRuns()
        {
            return await _runService.GetAllRuns();
        }

        [HttpDelete]
        [Authorize("delete:runs")]
        public async Task<bool> DeleteRun(string runId)
        {
            return await _runService.DeleteRun(runId);
        }

        [HttpPost("AddRunner")]
        [Authorize("create:runs")]
        public async Task AddRunner(string runId, string runnerId)
        {
            await _runService.AddRunner(runId, runnerId);
        }
    }
}
