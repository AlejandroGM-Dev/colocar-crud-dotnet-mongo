using ColocarCrud.Infrastructure.Data;
using ColocarCrud.Infrastructure.Models;
using ColocarCrud.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColocarCrud.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoContext context, IOptions<MongoSettings> options)
        {
            var collectionName = options.Value.UsersCollection;
            _users = context.Database.GetCollection<User>(collectionName);

            // indice uinco por email
            var indexKeys = Builders<User>.IndexKeys.Ascending(u => u.Email);
            var indexOptions = new CreateIndexOptions { Unique = true };
            _users.Indexes.CreateOne(new CreateIndexModel<User>(indexKeys, indexOptions));
        }

        public async Task<List<User>> GetAllAsync (CancellationToken ct) =>
            await _users.Find(_ => true).SortByDescending(u => u.CreatedAtUtc).ToListAsync(ct);

        public async Task<User?> GetByIdAsync(string id, CancellationToken ct) =>
            await _users.Find(u => u.Id == id).FirstOrDefaultAsync(ct);

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct) =>
            await _users.Find(u => u.Email == email).FirstOrDefaultAsync(ct);

        public async Task<User> CreateAsync(User user, CancellationToken ct)
        {
            await _users.InsertOneAsync(user, cancellationToken: ct);
            return user;
        }

        public async Task<bool> UpdateAsync(User user, CancellationToken ct)
        {
            var result = await _users.ReplaceOneAsync(u => u.Id == user.Id, user, cancellationToken: ct);
            return result.ModifiedCount == 1;
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken ct)
        {
            var result = await _users.DeleteOneAsync(u => u.Id == id, ct);
            return result.DeletedCount == 1;
        }
    }
}
