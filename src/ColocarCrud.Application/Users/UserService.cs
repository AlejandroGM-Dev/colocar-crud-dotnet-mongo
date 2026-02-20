using ColocarCrud.Infrastructure.Models;
using ColocarCrud.Infrastructure.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ColocarCrud.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo) => _repo = repo;

        public async Task<List<UserDto>> GetAllAsync(CancellationToken ct)
        => (await _repo.GetAllAsync(ct)).Select(u => new UserDto(u.Id!, u.Email, u.FullName, u.CreatedAtUtc)).ToList();
        
        public async Task<UserDto?> GetByIdAsync(string id, CancellationToken ct)
        {
            var u = await _repo.GetByIdAsync(id, ct);
            return u is null ? null : new UserDto(u.Id!, u.Email, u.FullName, u.CreatedAtUtc);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken ct)
        {
            Validate(dto.Email, dto.FullName);

            var user = new User
            {
                Email = NormalizeEmail(dto.Email),
                FullName = dto.FullName.Trim()
            };

            try
            {
                var created = await _repo.CreateAsync(user, ct);
                return new UserDto(created.Id!, created.Email, created.FullName, created.CreatedAtUtc);
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                throw new InvalidOperationException("El email ya existe.");
            }
        }

        public async Task<bool> UpdateAsync(UpdateUserDto dto, CancellationToken ct)
        {
            Validate(dto.Email, dto.FullName);

            var existing = await _repo.GetByIdAsync(dto.Id, ct);
            if (existing is null) return false;

            existing.Email = NormalizeEmail(dto.Email);
            existing.FullName = dto.FullName.Trim();

            try
            {
                return await _repo.UpdateAsync(existing, ct);
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                throw new InvalidOperationException("El email ya existe.");
            }
        }

        public Task<bool> DeleteAsync(string id, CancellationToken ct)
            => _repo.DeleteAsync(id, ct);

        private static string NormalizeEmail(string email) => email.Trim().ToLowerInvariant();

        private static void Validate(string email, string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName) || fullName.Trim().Length < 3)
                throw new ArgumentException("Nombre inválido (mínimo 3 caracteres).");

            var ok = Regex.IsMatch(email ?? "", @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!ok) throw new ArgumentException("Email inválido.");
        }

    }
}
