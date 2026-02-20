using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColocarCrud.Infrastructure.Settings
{
    public class MongoSettings
    {
        public required string ConnectionString { get; set; }
        public required string Database { get; set; }
        public required string UsersCollection { get; set; }

    }
}
