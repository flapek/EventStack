using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStack_API.Interfaces
{
    public interface IDbModelValidator
    {
        bool Validate(IDbModel dbModel);
    }
}
