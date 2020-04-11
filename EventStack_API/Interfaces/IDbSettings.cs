using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStack_API.Interfaces
{
    public interface IDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
