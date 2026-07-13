using Microsoft.EntityFrameworkCore;
using School.Models;

namespace School.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Secretary> Secretaries { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<ClassSubject> ClassSubjects { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Secretary>()
                .HasIndex(s => s.Cpf)
                .IsUnique();

            modelBuilder.Entity<Secretary>()
                .HasIndex(s => s.Email)
                .IsUnique();

            modelBuilder.Entity<Subject>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Teacher>()
                .HasIndex(t => t.Cpf)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Cpf)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Registration)
                .IsUnique();

            modelBuilder.Entity<Student>()
               .HasOne(s => s.Class)
               .WithMany(t => t.Students)
               .HasForeignKey(s => s.ClassId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Class>()
                .Property(c => c.SchoolGrade)
                .HasConversion<string>();

            modelBuilder.Entity<ClassSubject>()
                .HasIndex(cs => new { cs.ClassId, cs.SubjectId })
                .IsUnique();


            modelBuilder.Entity<ClassSubject>()
                .HasOne(cs => cs.Class)
                .WithMany(c => c.ClassSubjects)
                .HasForeignKey(cs => cs.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClassSubject>()
                .HasOne(cs => cs.Subject)
                .WithMany(s => s.ClassSubjects)
                .HasForeignKey(cs => cs.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClassSubject>()
                .HasOne(cs => cs.Teacher)
                .WithMany(t => t.ClassSubjects)
                .HasForeignKey(cs => cs.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .Property(g => g.Quarter)
                .HasConversion<string>();

            modelBuilder.Entity<Grade>()
                .HasIndex(g => new { g.StudentId, g.ClassSubjectId, g.Quarter })
                .IsUnique();

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.ClassSubject)
                .WithMany(cs => cs.Grades)
                .HasForeignKey(g => g.ClassSubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .Ignore(g => g.Average);

            modelBuilder.Entity<AuditLog>()
                .HasOne(a => a.PerformedBySecretary)
                .WithMany(s => s.AuditLogs)
                .HasForeignKey(a => a.PerformedBySecretaryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Secretary>().HasData(new Secretary
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Secretário Teste",
                Email = "secretario@escola.com",
                Cpf = "00000000000",
                PasswordHash = "$2a$11$J0sLkJlofV8PhWE7GgrbI.STAymSuhVwskjEH1Uh5KZdRrWd.1rle"
            });
        }
    }
}
