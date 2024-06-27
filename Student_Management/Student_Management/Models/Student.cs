using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
	public class Student
	{
		[Key]
		public int Id { get; set; }

		//Tên học sinh
		[Required]
		public string Name { get; set; }

		//Ngày sinh học sinh
		[Required]
		public DateTime DateOfBirth { get; set; }

		// Quan hệ 1-n với Enrollment
		public ICollection<Enrollment>? Enrollments { get; set; }
		
		//Quan hệ 1-n với Score
		public ICollection<Score>? Scores { get; set; }
	}
}
