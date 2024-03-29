﻿using MongoDB.Bson;
using MongoDB.Driver;

namespace PortStanleyRun.Api.Repositories.Interfaces
{
    public interface IRunRepository
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
        /// <param name="startingPoint">Starting point for the participant for this run</param>
        /// <returns>Result of the update</returns>
        Task<bool> AddRunner(string runId, string runnerId, string startingPoint);

        /// <summary>
        /// Delete a Port Stanley Run
        /// </summary>
        /// <param name="runId">ObjectId of the run to be deleted</param>
        /// <returns>true if the run was deleted, false otherwise</returns>
        Task<bool> DeleteRun(string runId);
    }
}
