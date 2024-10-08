﻿using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
	public class Score
	{
		//Điểm số
		[Required]
		public double Mark { get; set; }

		// Khóa ngoại đến Subject
		public int? SubjectId { get; set; }
		public Subject Subject { get; set; }

		// Khóa ngoại đến Student
		public int? StudentId { get; set; }
		public Student Student { get; set; }

        // Khóa ngoại đến Course
        public int? CourseId { get; set; }
        public Course Course { get; set; }
    }
}
