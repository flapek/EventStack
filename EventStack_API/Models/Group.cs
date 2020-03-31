using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Group
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId id { get; set; }
        public CoverPhoto cover { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string icon { get; set; }
        public int member_count { get; set; }
        public int member_request_count { get; set; }
        public string name { get; set; }
        //public ?Organization? owner { get; set; }
        public Group parent { get; set; }
        public string permissions { get; set; }
        public string privacy { get; set; }
        public DateTime updated_time { get; set; }
    }
}