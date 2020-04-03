using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStack_API.Interfaces
{
    public interface IBaseDbModel
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
