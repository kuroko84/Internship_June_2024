using Microsoft.EntityFrameworkCore;
using Student_Management.Models;
using System;
using System.Linq;

namespace Student_Management.DBContext
{
	public class SeedData
	{
		public static void SeedingData(StudentDbContext _context)
		{
			_context.Database.Migrate();
			if (!_context.Enrollments.Any())
			{
				//Khởi tạo môn học
				Subject mathematics = new Subject
				{
					Name = "Mathematics",
					Description = "Study of numbers, quantities, and shapes."
				};
				Subject physics = new Subject
				{
					Name = "Physics",
					Description = "Study of matter, energy, and the interaction between them."
				};

				// Khởi tạo lớp của sinh viên
				ClassOfStudent classOfStudent = new ClassOfStudent()
				{
					Name = "HTCL 2024",
					Description = "He Thong Thong Tin Chat Luong Cao 2024",
					SchoolYear = 2024
				};				

				//Khởi tạo sinh viên
				Student john = new Student
				{
					Name = "John Doe",
					DateOfBirth = new DateTime(2000, 1, 1),
					ClassOfStudent = classOfStudent,
				};
				Student jane = new Student
				{
					Name = "Jane Smith",
					DateOfBirth = new DateTime(2001, 2, 2),
                    ClassOfStudent = classOfStudent,
                };

				//Khởi tạo lớp
				var mathClass = new Course
				{
					Name = "Math Class 101",
					Description = "Introduction to Mathematics",
					NumberOfStudent = 50,
					StartDay = DateTime.Now,
					EndDay = DateTime.Now.AddMonths(3),
					Subject = mathematics
				};

				var physicsClass = new Course
                {
					Name = "Physics Class 101",
					Description = "Introduction to Physics",
					NumberOfStudent = 50,
					StartDay = DateTime.Now,
					EndDay = DateTime.Now.AddMonths(3),
					Subject = physics
				};

				_context.Courses.AddRange(mathClass, physicsClass);

				//Khởi tạo điểm số
				_context.Scores.AddRange(
					new Score
					{
						Subject = physics,
						Student = john,
                        Course = physicsClass,
                        Mark = 9
					},
					new Score
					{
						Subject = mathematics,
						Student = jane,
                        Course = mathClass,
                        Mark = 9.5
					}
				);

				//Khởi tạo đăng ký enrollment
				_context.Enrollments.AddRange(
					new Enrollment
					{
						Student = john,
                        Course = physicsClass,
						Status = 1
					},
					new Enrollment
					{
						Student = jane,
                        Course = mathClass,
						Status = 1
					}
				);

				_context.SaveChanges();
			}
		}
	}
}
