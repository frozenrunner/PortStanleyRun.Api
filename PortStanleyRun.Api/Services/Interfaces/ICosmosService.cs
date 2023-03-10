using PortStanleyRun.Api.Models;

namespace PortStanleyRun.Api.Services.Interfaces { 
    public interface ICosmosService
    {
        Task AddUser(PortStanleyUser user);
    }
}