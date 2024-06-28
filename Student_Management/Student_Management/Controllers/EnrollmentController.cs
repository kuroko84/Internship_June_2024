using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management.DBContext;

namespace Student_Management.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly StudentDbContext _studentDbContext;

        // Lâu lâu lỗi constructor, tắt mở lại :))))
        public EnrollmentController(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }

        public IActionResult Index()
        {
            var enrollments = _studentDbContext.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Class)
                    .ThenInclude(c => c.Subject)
                .ToList();

            return View(enrollments); // Truyền dữ liệu enrollments vào view
        }
    }
}
