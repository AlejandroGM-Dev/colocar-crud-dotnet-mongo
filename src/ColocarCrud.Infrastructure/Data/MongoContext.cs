using ColocarCrud.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColocarCrud.Infrastructure.Data
{
    public class MongoContext
    {
        public IMongoDatabase Database { get; }

        public MongoContext(IOptions<MongoSettings> options)
        {
            var settings = options.Value;
            var client = new MongoClient(settings.ConnectionString);
            Database = client.GetDatabase(settings.Database);
        }
    }
}
