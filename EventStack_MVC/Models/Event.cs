using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Models;

namespace Models
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public int attending_count { get; set; }
        public bool can_guests_invite { get; set; }
        public CategoryType category { get; set; }
        public CoverPhoto cover { get; set; }
        public int declined_count { get; set; }
        public string description { get; set; }
        public bool discount_code_enabled { get; set; }
        public string end_time { get; set; }
        public List<ChildEvent> event_times { get; set; }
        public bool guest_list_enabled { get; set; }
        public int interested_count { get; set; }
        public bool is_canceled { get; set; }
        public bool is_draft { get; set; }
        public bool is_page_owned { get; set; }
        public int maybe_count { get; set; }
        public string name { get; set; }
        public int noreply_count { get; set; }
        //public ??? owner { get; set; }
        public Group parent_group { get; set; }
        public Place place { get; set; }
        public string schedulded_publish_time { get; set; }
        public string starting_time { get; set; }
        public string start_time { get; set; }
        public string ticket_uri { get; set; }
        public string ticket_uri_start_sales_time { get; set; }
        public string ticketing_privacy_uri { get; set; }
        public string ticketing_terms_uri { get; set; }
        // public ??? timezone { get; set; }
        public EventType type { get; set; }
        public DateTime updated_time { get; set; }
    }
}