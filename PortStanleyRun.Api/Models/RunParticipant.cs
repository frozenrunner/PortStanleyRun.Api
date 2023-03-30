using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PortStanleyRun.Api.Models
{
    public class RunParticipant
    {
        /// <summary>
        /// ObjectId of a PortStanleyUser
        /// </summary>
        [BsonElement("runnerId")]
        public ObjectId RunnerId { get; set; }

        /// <summary>
        /// Starting point for the run this shows up in
        /// </summary>
        [BsonElement("startingPoint")]
        public string StartingPoint { get; set; } = string.Empty;
    }
}
