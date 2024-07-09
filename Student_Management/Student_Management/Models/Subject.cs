using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
	public class Subject
	{
		[Key]
		public int Id { get; set; }

		//Tên môn học
		[Required]
		public string Name { get; set; }

		//Mô tả môn học
		[Required]
		public string Description { get; set; }

		//Quan hệ 1-n với class
		public ICollection<Course>? Courses { get; set; }

		//Quan hệ 1-n với score
		public ICollection<Score>? Scores { get; set; }
	}
}
