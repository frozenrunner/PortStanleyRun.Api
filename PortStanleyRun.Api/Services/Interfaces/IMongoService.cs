using PortStanleyRun.Api.Models;

namespace PortStanleyRun.Api.Services.Interfaces { 
    public interface IMongoService
    {
        Task AddUser(PortStanleyUser user);
    }
}