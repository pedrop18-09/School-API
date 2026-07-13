using School.Data;
using School.Models;
using School.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace School.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _context;

        public TeacherRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            return await _context.Teachers
                .Where(t => t.IsActive)
                .ToListAsync();
        }

        public async Task<Teacher?> GetByIdAsync(Guid id)
        {
            return await _context.Teachers.FindAsync(id);
        }

        public async Task<List<Teacher>> GetInactiveAsync()
        {
            return await _context.Teachers
                .Where(t => !t.IsActive)
                .ToListAsync();
        }

        public async Task<Teacher?> GetByCpfAsync(string cpf)
        {
            return await _context.Teachers.FirstOrDefaultAsync(t => t.Cpf == cpf);
        }

        public async Task<bool> ExistsByCpfAsync(string cpf)
        {
            return await _context.Teachers.AnyAsync(t => t.Cpf == cpf);
        }

        public async Task AddAsync(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }
        }
    }
}
