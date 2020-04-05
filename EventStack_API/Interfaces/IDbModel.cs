﻿using MongoDB.Bson;

namespace EventStack_API.Interfaces
{
    public interface IDbModel
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
