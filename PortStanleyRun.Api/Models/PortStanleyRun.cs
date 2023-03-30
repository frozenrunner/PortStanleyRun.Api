﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PortStanleyRun.Api.Models
{
    public class PortStanleyRun
    {
        /// <summary>
        /// MongoDB id for document
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        /// <summary>
        /// String representation of a MongoDb BsonId
        /// </summary>
        public string ObjectIdString { get
            {
                return Id.ToString();
            } 
        }

        /// <summary>
        /// Date of a run
        /// </summary>
        public DateTime RunDate { get; set; }

        /// <summary>
        /// List of participants for a run
        /// </summary>
        public List<RunParticipant> Runners { get; set; } = new List<RunParticipant>();
    }
}