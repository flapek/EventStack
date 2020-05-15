using System;
using System.Linq;

namespace EventStack_API.Helpers
{
    public static class SecretGenerator
    {
        private static readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random random = new Random();

        public static string Generate() => RandomString(64);

        public static string RandomString(int length) => new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
