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
		public DbSet<Course> Courses { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<Score> Scores { get; set; }
		public DbSet<ClassOfStudent> ClassOfStudents { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Mối quan hệ 1-n giữa Student và Clas
			modelBuilder.Entity<Student>()
				.HasOne(s => s.ClassOfStudent)
				.WithMany(cs => cs.Students)
				.HasForeignKey(s => s.ClassOfStudentId)
                .OnDelete(DeleteBehavior.Restrict); ;

            // Mối quan hệ n-n giữa Student, Course, Subject qua bảng Score
            modelBuilder.Entity<Score>()
                .HasKey(e => new { e.StudentId, e.CourseId, e.SubjectId });

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
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Scores)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Mối quan hệ n-n giữa Student và Course qua bảng Enrollment
            modelBuilder.Entity<Enrollment>()
				.HasKey(e => new { e.StudentId, e.CourseId });

			modelBuilder.Entity<Enrollment>()
				.HasOne(e => e.Student)
				.WithMany(s => s.Enrollments)
				.HasForeignKey(e => e.StudentId)
				.OnDelete(DeleteBehavior.Restrict); 

			modelBuilder.Entity<Enrollment>()
				.HasOne(e => e.Course)
				.WithMany(c => c.Enrollments)
				.HasForeignKey(e => e.CourseId)
				.OnDelete(DeleteBehavior.Restrict); 

			base.OnModelCreating(modelBuilder);
		}
	}
}
