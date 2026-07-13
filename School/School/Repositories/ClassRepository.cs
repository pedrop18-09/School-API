using School.Data;
using School.Models;
using School.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace School.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext _context;

        public ClassRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Class>> GetAllAsync()
        {
            return await _context.Classes
                .Include(c => c.Students)
                .ToListAsync();
        }

        public async Task<Class?> GetByIdAsync(Guid id)
        {
            return await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Class schoolClass)
        {
            await _context.Classes.AddAsync(schoolClass);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Class schoolClass)
        {
            _context.Classes.Update(schoolClass);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var schoolClass = await _context.Classes.FindAsync(id);
            if (schoolClass != null)
            {
                _context.Classes.Remove(schoolClass);
                await _context.SaveChangesAsync();
            }
        }
    }
}