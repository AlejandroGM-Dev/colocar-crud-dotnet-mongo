using ColocarCrud.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColocarCrud.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(CancellationToken ct);
        Task<User?> GetByIdAsync(string id, CancellationToken ct);
        Task<User?> GetByEmailAsync(string email, CancellationToken ct);
        Task<User> CreateAsync(User user, CancellationToken ct);
        Task<bool> UpdateAsync(User user, CancellationToken ct);
        Task<bool> DeleteAsync(string id, CancellationToken ct);
    }
}
