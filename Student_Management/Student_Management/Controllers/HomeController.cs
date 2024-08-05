using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management.DBContext;
using Student_Management.Models;
using System.Diagnostics;

namespace Student_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentDbContext _studentDbContext;

        public HomeController(ILogger<HomeController> logger, StudentDbContext studentDbContext)
        {
            _logger = logger;
            _studentDbContext = studentDbContext;
        }

        public async Task<IActionResult> GetDashBoard()
        {
            var allStudents = await _studentDbContext.Students.ToListAsync();
            var allCourses = await _studentDbContext.Courses.ToListAsync();
            var allClasses = await _studentDbContext.ClassOfStudents.ToListAsync();
            var allScores = await _studentDbContext.Scores.ToListAsync();
            var allSubjects = await _studentDbContext.Subjects.ToListAsync();
            var allEnrollments = await _studentDbContext.Enrollments.ToListAsync();

            var dashboardData = new
            {
                Students = allStudents,
                Courses = allCourses,
                Classes = allClasses,
                Scores = allScores,
                Subjects = allSubjects,
                Enrollments = allEnrollments
            };

            return Json(dashboardData);
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
