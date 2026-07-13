using School.Data;
using School.Models;
using School.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using School.Enums;

namespace School.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly AppDbContext _context;

        public GradeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Grade>> GetAllAsync()
        {
            return await _context.Grades.ToListAsync();
        }

        public async Task<Grade?> GetByIdAsync(Guid id)
        {
            return await _context.Grades.FindAsync(id);
        }

        public async Task<List<Grade>> GetByStudentIdAsync(Guid studentId)
        {
            return await _context.Grades
                .Include(g => g.ClassSubject)
                    .ThenInclude(cs => cs.Subject)
                .Include(g => g.ClassSubject)
                    .ThenInclude(cs => cs.Teacher)
                .Include(g => g.ClassSubject)
                    .ThenInclude(cs => cs.Class)      // NOVO
                .Where(g => g.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<List<Grade>> GetByClassSubjectIdAsync(Guid classSubjectId)
        {
            return await _context.Grades
                .Include(g => g.Student)
                .Where(g => g.ClassSubjectId == classSubjectId)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid studentId, Guid classSubjectId, Quarter quarter)
        {
            return await _context.Grades
                .AnyAsync(g => g.StudentId == studentId && g.ClassSubjectId == classSubjectId && g.Quarter == quarter);
        }

        public async Task AddAsync(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Grade grade)
        {
            _context.Grades.Update(grade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();
            }
        }
    }
}