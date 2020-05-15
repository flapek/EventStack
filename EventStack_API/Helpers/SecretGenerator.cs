using System;

namespace EventStack_API.Helpers
{
    public static class SecretGenerator
    {
        public static string Generate() => new Guid().ToString();
    }
}
