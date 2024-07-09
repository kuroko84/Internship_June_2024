using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
    public class ClassOfStudent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int SchoolYear { get; set; }   

        // Quan hệ 1-n với student
        public ICollection<Student> Students { get; set; }
    }
}
