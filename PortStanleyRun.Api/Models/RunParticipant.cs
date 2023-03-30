using MongoDB.Bson;

namespace PortStanleyRun.Api.Models
{
    public class RunParticipant
    {
        /// <summary>
        /// ObjectId of a PortStanleyUser
        /// </summary>
        public ObjectId RunnerId { get; set; }

        /// <summary>
        /// Starting point for the run this shows up in
        /// </summary>
        public string StartingPoint { get; set; } = string.Empty;
    }
}
