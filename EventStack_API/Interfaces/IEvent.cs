using System.Collections.Generic;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Geolocation;
using System.Security.Policy;

namespace Interface
{
    public interface IEvent
    {
        [BsonId]
        [BsonElement("Id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Categories")]
        public IEnumerable<ICategory> Categories  { get; set; }
        [BsonElement("Photo")]
        public string Photo { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Startime")]
        public DateTime StartTime { get; set; }
        [BsonElement("EndTime")]
        public DateTime EndTime { get; set; }
        [BsonElement("PublishTime")]
        public DateTime PublishTime { get; set; }
        [BsonElement("Place")]
        public Coordinate Place { get; set; }
        [BsonElement("IsCanceled")]
        public bool IsCanceled { get; set; }
        [BsonElement("FacebookUrl")]
        public Url FacebookUrl { get; set; }
        [BsonElement("WebSiteUrl")]
        public Url WebSiteUrl { get; set; }
    }
}