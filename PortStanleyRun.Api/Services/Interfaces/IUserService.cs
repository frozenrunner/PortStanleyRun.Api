using PortStanleyRun.Api.Models;

namespace PortStanleyRun.Api.Services.Interfaces { 
    public interface IUserService
    {
        /// <summary>
        /// Get user, if they exist
        /// </summary>
        /// <param name="userName">The name of the user to search for</param>
        /// <returns>User data</returns>
        Task<PortStanleyUser> GetUser(string userName);

        /// <summary>
        /// Add a user to the database
        /// </summary>
        /// <param name="user">The user to add to the database</param>
        Task AddUser(PortStanleyUser user);
    }
}