using MongoDB.Bson;

namespace EventStack_MVC.Interfaces
{
    public interface IDbModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
