using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Student_Management.DBContext;
using Student_Management.Models;
using System.Linq;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Student_Management.Controllers
{
    public class CourseController : Controller
    {
        private readonly StudentDbContext _studentDbContext;
        private readonly ILogger<CourseController> _logger;

        public CourseController(StudentDbContext studentDbContext, ILogger<CourseController> logger)
        {
            _studentDbContext = studentDbContext;
            _logger = logger;
        }

        // GET: /Course/Create
        [HttpGet]
        public async Task<IActionResult> AddCourse()
        {
            ViewData["Title"] = "Create";

            // Lấy danh sách các môn học từ database và gán vào ViewBag
            ViewBag.Subjects = new SelectList(await _studentDbContext.Subjects.ToListAsync(), "Id", "Name");

            return View();
        }

        // POST: /Course/AddCourse
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromForm] Course course)
        {
            // Thêm mới đối tượng Course và lưu vào cơ sở dữ liệu
            Course newCourse = new Course
            {
                Name = course.Name,
                Description = course.Description,
                NumberOfStudent = course.NumberOfStudent,
                StartDay = course.StartDay,
                EndDay = course.EndDay,
                SubjectId = course.SubjectId, // Đảm bảo SubjectId được bind từ form
            };

            _studentDbContext.Add(newCourse);
            await _studentDbContext.SaveChangesAsync();

            _logger.LogInformation("Course created successfully.");
            return RedirectToAction("Index", "Home");

        }

        //GET Course/AranageCourse
        public async Task<IActionResult> ArrangeCourse(int Id)
        {
            // Truy vấn lớp được chọn
            var selectedCourse = await _studentDbContext.Courses
                .Include(c => c.Subject) // Bao gồm thông tin môn học của lớp
                .SingleOrDefaultAsync(e => e.Id == Id);
            ViewData["Title"] = "Add Student To";

            var studentsInCourse = await _studentDbContext.Students
                .Where(s => s.Enrollments.Any(e => e.CourseId == Id && e.Status != 0)) // Sinh viên thuộc lớp học này
                .Include(s => s.Scores
                    .Where(score => score.CourseId == selectedCourse.Id)) // Chỉ lấy điểm cho môn học của lớp và sinh viên thuộc lớp này
                .ToListAsync();

            // Lấy danh sách sinh viên không nằm trong lớp
            var studentsNotInCourse = await _studentDbContext.Students
                .Where(s => !s.Enrollments.Any(e => e.CourseId == Id))
                .ToListAsync();

            // Chuyển đổi danh sách sinh viên không nằm trong lớp thành SelectListItem
            var studentsNotInCourseItems = studentsNotInCourse.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(), // Giá trị của option
                Text = s.Name // Tên hiển thị của option
            }).ToList();

            // Chuyển đổi danh sách sinh viên sang dạng phù hợp để truyền vào ViewBag
            var studentWithScores = studentsInCourse.Select(s => new
            {
                s.Id,
                s.Name,
                SubjectId = selectedCourse.SubjectId,
                // Lấy điểm số cho môn học của lớp
                // Hiển thị -1 nếu chưa có điểm, ko khởi tạo
                Mark = s.Scores.FirstOrDefault(score => score.SubjectId == selectedCourse.SubjectId)?.Mark ?? -1
            }).ToList();
            //Student ko có trong Course để có thể thêm vào Course
            ViewBag.Students = studentsNotInCourseItems;

            //Student đã có trong Course, ko thêm
            ViewBag.StudentInCourse = studentWithScores;
            ViewBag.CourseId = Id;
            ViewBag.Name = selectedCourse.Name;

            _logger.LogInformation("Course collected.", Id);

            return View();
        }


        // POST: /course/AddAranageCourse
        [HttpPost]
        public async Task<IActionResult> AddArrangeCourse(Enrollment enroll)
        {
            _logger.LogInformation("Received Enrollment: {@student}", enroll);

            // Tạo 1 enroll
            if (enroll.StudentId.HasValue)
            {
                Enrollment newEroll = new Enrollment
                {
                    CourseId = enroll.CourseId,
                    StudentId = enroll.StudentId,
                };
                _studentDbContext.Enrollments.Add(newEroll);
                await _studentDbContext.SaveChangesAsync();
            }
            else
            {
                TempData["InvalidData"] = "Please choose a student";
            }

            return RedirectToAction("ArrangeCourse", "Course", new { Id = enroll.CourseId });
        }

        //POST /Course/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Course/UpdateScore
        [HttpPost]
        public async Task<IActionResult> UpdateScore(int studentId, double mark, int CourseId, int subjectId)
        {
            try
            {
                var scoreToUpdate = await _studentDbContext.Scores
                    .SingleOrDefaultAsync(s => s.StudentId == studentId && s.CourseId == CourseId);

                if (scoreToUpdate != null)
                {
                    // Đã có điểm số, cập nhật lại
                    scoreToUpdate.Mark = mark;
                }
                else
                {
                    // Chưa có điểm số, tạo mới và thêm vào
                    scoreToUpdate = new Score
                    {
                        Mark = mark,
                        SubjectId = subjectId,
                        StudentId = studentId,
                        CourseId = CourseId
                    };

                    _studentDbContext.Scores.Add(scoreToUpdate);
                }

                await _studentDbContext.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        // POST: Course/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                Course courseToDelete = _studentDbContext.Courses.FirstOrDefault(x => x.Id == Id);
                if (courseToDelete == null)
                {
                    return NotFound();
                }

                _studentDbContext.Courses.Remove(courseToDelete);
                await _studentDbContext.SaveChangesAsync();

                TempData["DeleteCourseSuccess"] = "Course deleted successfully.";

                // Chuyển Index
                return RedirectToAction("Index", "Student");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // POST: Course/DeleteStudentOfCourse
        [HttpPost]
        public async Task<IActionResult> DeleteStudentOfCourse(int Id, int courseId)
        {
            try
            {
                // Tìm Enrollment dựa vào studentId và CourseId
                var enrollmentOfStudentToUpdate = await _studentDbContext.Enrollments
                    .FirstOrDefaultAsync(x => x.StudentId == Id && x.CourseId == courseId);

                if (enrollmentOfStudentToUpdate == null)
                {
                    return NotFound();
                }

                // Cập nhật trường Status thành 0
                enrollmentOfStudentToUpdate.Status = 0;

                await _studentDbContext.SaveChangesAsync();


                // Chuyển View
                return RedirectToAction("AranageCourse", "Course", new { Id = courseId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
