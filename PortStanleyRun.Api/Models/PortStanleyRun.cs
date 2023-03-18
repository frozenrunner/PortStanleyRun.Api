using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace PortStanleyRun.Api.Models
{
    public class PortStanleyRun
    {
        /// <summary>
        /// MongoDB id for document
        /// </summary>
        [BsonId]
        public ObjectId _id { get; set; } = ObjectId.GenerateNewId();

        public string ObjectIdString { get
            {
                return _id.ToString();
            } 
        }

        /// <summary>
        /// Date of a run
        /// </summary>
        public DateTime RunDate { get; set; }

        public IEnumerable<ObjectId> Runners { get; set; } = new List<ObjectId>();
    }
}