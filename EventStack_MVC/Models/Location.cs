using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class Location
    {
        public string city { get; set; }
        public uint city_id { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public float latitude { get; set; }
        // public ??? located_in { get; set; }
        public float longitude { get; set; }
        public string name { get; set; }
        public string region { get; set; }
        public uint region_id { get; set; }
        public string state { get; set; }
        public string street { get; set; }
        public string zip { get; set; }
    }
}