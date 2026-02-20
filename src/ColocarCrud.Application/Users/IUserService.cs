using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColocarCrud.Application.Users
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync(CancellationToken ct);
        Task<UserDto?> GetByIdAsync(string id, CancellationToken ct);
        Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken ct);
        Task<bool> UpdateAsync(UpdateUserDto dto, CancellationToken ct);
        Task<bool> DeleteAsync(string id, CancellationToken ct);
    }
}
