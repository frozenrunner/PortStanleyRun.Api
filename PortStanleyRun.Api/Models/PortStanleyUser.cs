using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PortStanleyRun.Api.Models
{
    public class PortStanleyUser
    {
        /// <summary>
        /// MongoDB id for document
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        /// <summary>
        /// String representation of a MongoDb BsonId
        /// </summary>
        public string ObjectIdString
        {
            get
            {
                return Id.ToString();
            }
        }

        /// <summary>
        /// User name
        /// </summary>
        [BsonElement("userName")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Expected pace
        /// </summary>
        public string Pace { get; set; } = string.Empty;

        /// <summary>
        /// Runs that the user has been or plans to be part of
        /// </summary>
        public IEnumerable<ObjectId> Runs { get; set; } = new List<ObjectId>();
    }
}
