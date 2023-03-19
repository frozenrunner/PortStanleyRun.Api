using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using PortStanleyRun.Api.Models;
using PortStanleyRun.Api.Services;
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
        public async Task AddRun(Models.PortStanleyRun newRun)
        {
            await _runService.AddRun(newRun);
        }

        [HttpGet]
        public async Task<Models.PortStanleyRun> GetRun(string objectId)
        {
            return await _runService.GetRun(new ObjectId(objectId));
        }

        [HttpGet("GetAllRuns")]
        public async Task<List<Models.PortStanleyRun>> GetAllRuns()
        {
            return await _runService.GetAllRuns();
        }

        [HttpPost("AddRunner")]
        public async Task AddRunner(string runId, string runnerId)
        {
            await _runService.AddRunner(runId, runnerId);
        }
    }
}
