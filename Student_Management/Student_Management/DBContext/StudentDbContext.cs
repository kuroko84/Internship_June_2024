using Microsoft.EntityFrameworkCore;
using Student_Management.Models;

namespace Student_Management.DBContext
{
	public class StudentDbContext : DbContext
	{
		public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
		{
		}

		public DbSet<Student> Students { get; set; }
		public DbSet<Class> Classes { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<Score> Scores { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            // Mối quan hệ n-n giữa Student, Class, Subject qua bảng Score
            modelBuilder.Entity<Score>()
                .HasKey(e => new { e.StudentId, e.ClassId, e.SubjectId });

            modelBuilder.Entity<Score>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Scores)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Score>()
                .HasOne(sc => sc.Subject)
                .WithMany(sub => sub.Scores)
                .HasForeignKey(sc => sc.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Score>()
                .HasOne(sc => sc.Class)
                .WithMany(c => c.Scores)
                .HasForeignKey(sc => sc.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            // Mối quan hệ n-n giữa Student và Class qua bảng Enrollment
            modelBuilder.Entity<Enrollment>()
				.HasKey(e => new { e.StudentId, e.ClassId });

			modelBuilder.Entity<Enrollment>()
				.HasOne(e => e.Student)
				.WithMany(s => s.Enrollments)
				.HasForeignKey(e => e.StudentId)
				.OnDelete(DeleteBehavior.Restrict); // Thay vì Cascade

			modelBuilder.Entity<Enrollment>()
				.HasOne(e => e.Class)
				.WithMany(c => c.Enrollments)
				.HasForeignKey(e => e.ClassId)
				.OnDelete(DeleteBehavior.Restrict); // Thay vì Cascade

			base.OnModelCreating(modelBuilder);
		}
	}
}
