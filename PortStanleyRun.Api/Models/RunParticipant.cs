using MongoDB.Bson;

namespace PortStanleyRun.Api.Models
{
    public class RunParticipant
    {
        public ObjectId RunnerId { get; set; }
        public string StartingPoint { get; set; } = string.Empty;
    }
}
