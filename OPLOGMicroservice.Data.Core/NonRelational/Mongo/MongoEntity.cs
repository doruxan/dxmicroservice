using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OPLOGMicroservice.Data.Core.NonRelational.Mongo;
using System;

namespace OPLOGMicroservice.Data.Core.NonRelational
{
    public class MongoEntity : IMongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }

        /// <summary>
        /// for ordering purposes
        /// </summary>
        public long UnixTimestamp { get; set; }
    }
}
