using System.Collections.Generic;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Geolocation;

namespace Interfaces
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

        public IEnumerable<ICategory> Categories { get; set; }
        [BsonElement("Photo")]
        public string Photo { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("StarTime")]
        public DateTime StarTime { get; set; }
        [BsonElement("EndTime")]
        public DateTime EndTime { get; set; }
        [BsonElement("PublishTime")]
        public DateTime PublishTime { get; set; }
        [BsonElement("Place")]
        public Coordinate Place { get; set; }
        [BsonElement("IsCanceled")]
        public bool IsCanceled { get; set; }
        [BsonElement("FacebookURL")]
        public Uri FacebookURL { get; set; }
        [BsonElement("WebSiteURL")]
        public Uri WebSiteURL { get; set; }
    }
}