using System.ComponentModel.DataAnnotations;
using System;

namespace Student_Management.Models
{
	public class Class
	{
		[Key]
		public int Id { get; set; }

		//Tên lớp học
		[Required]
		public string Name { get; set; }

		//Mô tả lớp
		public string Description { get; set; }

		//Só lượng sinh viên mỗi lớp
		[Required]
		public int NumberOfStudent {  get; set; }	

		//Ngày bắt đầu khai giảng
		[Required]
		public DateTime StartDay { get; set; }

		//Ngày kết thúc lớp
		[Required]
		public DateTime EndDay { get; set; }

        //Khóa ngoại đến môn học
        [Required]
        public int? SubjectId { get; set; }
		public Subject Subject { get; set; }

		//Quan hệ 1-n với Enrollment
		public ICollection<Enrollment>? Enrollments { get; set; }
	}
}
