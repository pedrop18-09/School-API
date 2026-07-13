using School.Data;
using School.Models;
using School.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace School.Repositories
{
    public class ClassSubjectRepository : IClassSubjectRepository
    {
        private readonly AppDbContext _context;

        public ClassSubjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClassSubject>> GetAllAsync()
        {
            return await _context.ClassSubjects
                .Include(cs => cs.Class)
                .Include(cs => cs.Subject)
                .Include(cs => cs.Teacher)
                .ToListAsync();
        }

        public async Task<ClassSubject?> GetByIdAsync(Guid id)
        {
            return await _context.ClassSubjects
                .Include(cs => cs.Class)
                .Include(cs => cs.Subject)
                .Include(cs => cs.Teacher)
                .FirstOrDefaultAsync(cs => cs.Id == id);
        }

        public async Task<ClassSubject?> GetByClassAndSubjectAsync(Guid classId, Guid subjectId)
        {
            return await _context.ClassSubjects
                .FirstOrDefaultAsync(cs => cs.ClassId == classId && cs.SubjectId == subjectId);
        }

        public async Task<bool> ExistsAsync(Guid classId, Guid subjectId)
        {
            return await _context.ClassSubjects
                .AnyAsync(cs => cs.ClassId == classId && cs.SubjectId == subjectId);
        }

        public async Task AddAsync(ClassSubject classSubject)
        {
            await _context.ClassSubjects.AddAsync(classSubject);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClassSubject classSubject)
        {
            _context.ClassSubjects.Update(classSubject);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var classSubject = await _context.ClassSubjects.FindAsync(id);
            if (classSubject != null)
            {
                _context.ClassSubjects.Remove(classSubject);
                await _context.SaveChangesAsync();
            }
        }
    }
}