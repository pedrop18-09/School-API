using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;
using School.Repositories.Interfaces;

namespace School.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.Class)
                .Where(s => s.IsActive)
                .ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(Guid id)
        {
            return await _context.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Student>> GetInactiveAsync()
        {
            return await _context.Students
                .Include(s => s.Class)
                .Where(s => !s.IsActive)
                .ToListAsync();
        }

        public async Task<Student?> GetByCpfAsync(string cpf)
        {
            return await _context.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync(s => s.Cpf == cpf);
        }

        public async Task<bool> ExistsByCpfAsync(string cpf)
        {
            return await _context.Students.AnyAsync(s => s.Cpf == cpf);
        }

        public async Task<bool> ExistsByRegistrationAsync(string registration)
        {
            return await _context.Students.AnyAsync(s => s.Registration == registration);
        }

        public async Task<List<Student>> GetByClassIdAsync(Guid classId)
        {
            return await _context.Students
                .Where(s => s.ClassId == classId)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}