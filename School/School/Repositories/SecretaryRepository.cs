using School.Data;
using School.Models;
using School.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace School.Repositories
{
    public class SecretaryRepository : ISecretaryRepository
    {
        private readonly AppDbContext _context;

        public SecretaryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Secretary>> GetAllAsync()
        {
            return await _context.Secretaries.ToListAsync();
        }

        public async Task<Secretary?> GetByIdAsync(Guid id)
        {
            return await _context.Secretaries.FindAsync(id);
        }

        public async Task<Secretary?> GetByEmailAsync(string email)
        {
            return await _context.Secretaries.FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Secretaries.AnyAsync(s => s.Email == email);
        }

        public async Task<bool> ExistsByCpfAsync(string cpf)
        {
            return await _context.Secretaries.AnyAsync(s => s.Cpf == cpf);
        }

        public async Task AddAsync(Secretary secretary)
        {
            _context.Secretaries.Add(secretary);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Secretary secretary)
        {
            _context.Secretaries.Update(secretary);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var secretary = await _context.Secretaries.FindAsync(id);
            if (secretary != null)
            {
                _context.Secretaries.Remove(secretary);
                await _context.SaveChangesAsync();
            }
        }
    }
}
