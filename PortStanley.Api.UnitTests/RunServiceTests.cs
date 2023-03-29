using Moq;
using PortStanleyRun.Api.Repositories.Interfaces;
using PortStanleyRun.Api.Services;
using PortStanleyRun.Api.Services.Interfaces;

namespace PortStanley.Api.UnitTests
{
    public class RunServiceTests
    {
        private Mock<IRunRepository> _runRepository;
        private IRunService _runService;
        [SetUp]
        public void Setup()
        {
            _runRepository = new Mock<IRunRepository>();
        }

        [Test]
        public async Task GetRun_ReturnOk()
        {
            _runRepository.Setup(rr => rr.GetRun(It.IsAny<string>())).ReturnsAsync(new PortStanleyRun.Api.Models.PortStanleyRun
            {
                _id = new MongoDB.Bson.ObjectId("641749070755dd35ac1f6352"),
                RunDate = DateTime.Now,
                Runners = new List<MongoDB.Bson.ObjectId>()
            });

            _runService = new RunService(_runRepository.Object);

            var result = await _runService.GetRun("ACB123");

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task GetAllRuns_ReturnOk()
        {
            _runRepository.Setup(rr => rr.GetAllRuns()).ReturnsAsync(new List<PortStanleyRun.Api.Models.PortStanleyRun>
            {
                new PortStanleyRun.Api.Models.PortStanleyRun
                {
                    _id = new MongoDB.Bson.ObjectId("641749070755dd35acff6352"),
                    RunDate = DateTime.Now,
                    Runners = new List<MongoDB.Bson.ObjectId>()
                },
                new PortStanleyRun.Api.Models.PortStanleyRun
                {
                    _id = new MongoDB.Bson.ObjectId("641749070755dd35ac1f6352"),
                    RunDate = DateTime.Now,
                    Runners = new List<MongoDB.Bson.ObjectId>()
                }
            });

            _runService = new RunService(_runRepository.Object);

            var result = await _runService.GetAllRuns();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.GreaterThan(1));
        }
    }
}