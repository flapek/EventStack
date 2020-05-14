using EventStack_API.Helpers;
using EventStack_API.Interfaces;
using EventStack_API.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventStack_API.Workers
{
    public class Organization_MongoRepository : MongoRepository<Organization>, IRepositoryFactory<Organization>
    {
        private MongoDbContext Context { get; set; }
        private IMongoCollection<Organization> Collection { get; set; }

        public Organization_MongoRepository(IDbContext context) : base(context)
        {
            Context = (MongoDbContext)context;
            Collection = Context.GetCollection<Organization>(typeof(Organization).Name);
        }

        public new Organization Find(string secret) => Collection.Find(x => x.Secret == secret).FirstOrDefault();

        public async Task<Organization> FindAsync(Filter filter)
            => await Collection.FindAsync(x => x.Name == filter.Name && x.Email == filter.Email).Result.FirstOrDefaultAsync();

        public new async Task<bool> InsertAsync(Organization insert)
        {
            if (insert == null)
                throw new ArgumentNullException(nameof(Organization));

            using var session = Context.MongoClient.StartSession();
            return await session.WithTransactionAsync(async (s, c) =>
            {
                try
                {
                    if (!await CheckIfTheGivenModelExist(insert))
                    {
                        insert.Secret = await CheckSecret();
                        insert.EventsId = new List<string>();
                        await Collection.InsertOneAsync(s, insert);
                        return true;
                    }
                    else
                    {
                        await s.AbortTransactionAsync();
                        return false;
                    }

                }
                catch (Exception)
                {
                    await s.AbortTransactionAsync();
                    return false;
                }

            }, new TransactionOptions(), CancellationToken.None);
        }

        private async Task<string> CheckSecret()
        {
            string result;
            do
            {
                result = SecretGenerator.Generate();
            } while (await Collection.FindAsync(x => x.Secret == result).Result.FirstOrDefaultAsync() != null);
            return result;
        }

        private async Task<bool> CheckIfTheGivenModelExist(Organization insert)
            => null == await Collection.FindAsync(x => (x.Name == insert.Name && x.Email == insert.Email) || x.NIP == insert.NIP || x.REGON == insert.REGON)
            .Result.FirstOrDefaultAsync() ? false : true;

        public class Filter
        {
            [BsonElement("Name")]
            [BsonRequired]
            [Required(ErrorMessage = "Name must be set!")]
            [StringLength(100, ErrorMessage = "The maximum number of character is 100!")]
            public string Name { get; set; }

            [BsonElement("Email")]
            [BsonRequired]
            [Required(ErrorMessage = "Email must be set!")]
            [StringLength(100, ErrorMessage = "The maximum number of character is 100!")]
            [RegularExpression(@"[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*",
                ErrorMessage = "Email must contain eg. example@example.com")]
            public string Email { get; set; }
        }
    }
}
