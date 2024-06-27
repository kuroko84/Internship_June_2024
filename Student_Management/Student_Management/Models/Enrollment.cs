namespace Student_Management.Models
{
    public class Enrollment
    {
        // Constructor để gán giá trị mặc định
        public Enrollment()
        {
            Status = 1;
        }

        // Khóa ngoại đến student
        public int? StudentId { get; set; }
        public Student Student { get; set; }

        // Khóa ngoại đến class
        public int? ClassId { get; set; }
        public Class Class { get; set; }

        // Trạng thái đăng ký
        // 0 - Đã hủy lớp
        // 1 - Chưa hủy lớp
        public int Status { get; set; }
    }
}
