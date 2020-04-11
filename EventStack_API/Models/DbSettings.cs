using EventStack_API.Interfaces;

namespace EventStack_API.Models
{
    public class DbSettings : IDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}