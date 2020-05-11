using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventStack_API.Helpers
{
    public static class SecretGenerator
    {
        public static string Generate() => new Guid().ToString();
    }
}
