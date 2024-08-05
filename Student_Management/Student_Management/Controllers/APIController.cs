using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Student_Management.DBContext;

namespace Student_Management.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APIController : Controller
    {
        private readonly StudentDbContext _studentDbContext;
        private readonly ILogger<StudentController> _logger;
        private readonly IDistributedCache _distributedCache;
        public APIController(StudentDbContext studentDbContext,
            ILogger<StudentController> logger,
            IDistributedCache distributedCache
            )
        {
            _studentDbContext = studentDbContext;
            _logger = logger;
            this._distributedCache = distributedCache;
        }
        // Get all students
        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var allStudents = await _studentDbContext.Students.ToListAsync();
            return Json(allStudents);
        }
        // Get all courses
        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var allCourses = await _studentDbContext.Courses.ToListAsync();
            return Json(allCourses);
        }
        // Get all classes
        [HttpGet("GetAllClasses")]
        public async Task<IActionResult> GetAllClasses()
        {
            var allClasses = await _studentDbContext.ClassOfStudents.ToListAsync();
            return Json(allClasses);
        }
        // Get all subjects
        [HttpGet("GetAllSubjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var allSubjects = await _studentDbContext.Subjects.ToListAsync();
            return Json(allSubjects);
        }
        // Get all enrollments
        [HttpGet("GetAllEnrollments")]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var allEnrollments = await _studentDbContext.Enrollments.ToListAsync();
            return Json(allEnrollments);
        }
    }
}
