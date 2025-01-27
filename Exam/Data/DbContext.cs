using Exam.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam.Data
{
    public class ExamDbContext : DbContext
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.LessonCode);
                entity.Property(e => e.LessonCode).HasColumnType("char(3)").IsRequired();
                entity.Property(e => e.LessonName).HasColumnType("varchar(30)").IsRequired();
                entity.Property(e => e.GradeLevel).HasColumnType("int");
                entity.Property(e => e.TeacherFirstName).HasColumnType("varchar(20)");
                entity.Property(e => e.TeacherLastName).HasColumnType("varchar(20)");
            });


            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StudentNumber);
                entity.Property(e => e.StudentNumber).HasColumnType("int").IsRequired();
                entity.Property(e => e.FirstName).HasColumnType("varchar(30)").IsRequired();
                entity.Property(e => e.LastName).HasColumnType("varchar(30)").IsRequired();
                entity.Property(e => e.GradeLevel).HasColumnType("int");
            });


            modelBuilder.Entity<Examination>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.LessonCode).HasColumnType("char(3)").IsRequired();
                entity.Property(e => e.StudentId).HasColumnType("int").IsRequired();
                entity.Property(e => e.ExamDate).HasColumnType("date").IsRequired();
                entity.Property(e => e.Score).HasColumnType("int");


                entity.HasOne(e => e.Lesson)
                      .WithMany(l => l.Examinations)
                      .HasForeignKey(e => e.LessonCode)
                      .HasPrincipalKey(l => l.LessonCode)
                      .OnDelete(DeleteBehavior.Cascade);


                entity.HasOne(e => e.Student)
                      .WithMany(s => s.Examinations)
                      .HasForeignKey(e => e.StudentId)
                      .HasPrincipalKey(s => s.StudentNumber)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
