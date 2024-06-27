using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Student_Management.DBContext;
using Student_Management.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Student_Management.Controllers
{
    public class ClassController : Controller
    {
        private readonly StudentDbContext _studentDbContext;
        private readonly ILogger<ClassController> _logger;

        public ClassController(StudentDbContext studentDbContext, ILogger<ClassController> logger)
        {
            _studentDbContext = studentDbContext;
            _logger = logger;
        }

        // GET: /Class/Create
        [HttpGet]
        public async Task<IActionResult> AddClass()
        {
            ViewData["Title"] = "Create";

            // Lấy danh sách các môn học từ database và gán vào ViewBag
            ViewBag.Subjects = new SelectList(await _studentDbContext.Subjects.ToListAsync(), "Id", "Name");

            return View();
        }

        // POST: /Class/AddClass
        [HttpPost]
        public async Task<IActionResult> AddClass([FromForm] Class @class)
        {
            // Xử lý khi ModelState hợp lệ
            // Thêm mới đối tượng Class và lưu vào cơ sở dữ liệu
            Class newClass = new Class
            {
                Name = @class.Name,
                Description = @class.Description,
                NumberOfStudent = @class.NumberOfStudent,
                StartDay = @class.StartDay,
                EndDay = @class.EndDay,
                SubjectId = @class.SubjectId, // Đảm bảo SubjectId được bind từ form
            };

            _studentDbContext.Add(newClass);
            await _studentDbContext.SaveChangesAsync();

            _logger.LogInformation("Class created successfully.");
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> AranageClass(int Id)
        {
            var selectedClass = await _studentDbContext.Classes
                .Include(c => c.Subject) // Bao gồm thông tin môn học của lớp
                .SingleOrDefaultAsync(e => e.Id == Id);
            ViewData["Title"] = "Add Student To";

            // Lấy danh sách sinh viên trong lớp, bao gồm điểm số và môn học của lớp
            var studentsInClass = await _studentDbContext.Students
                .Where(s => s.Enrollments.Any(e => e.ClassId == Id && e.Status != 0))
                .Include(s => s.Scores.Where(score => score.SubjectId == selectedClass.SubjectId)) // Chỉ lấy điểm cho môn học của lớp
                .ToListAsync();

            // Lấy danh sách sinh viên không nằm trong lớp
            var studentsNotInClass = await _studentDbContext.Students
                .Where(s => !s.Enrollments.Any(e => e.ClassId == Id))
                .ToListAsync();

            // Chuyển đổi danh sách sinh viên không nằm trong lớp thành SelectListItem
            var studentsNotInClassItems = studentsNotInClass.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(), // Giá trị của option
                Text = s.Name // Tên hiển thị của option
            }).ToList();

            // Chuyển đổi danh sách sinh viên sang dạng phù hợp để truyền vào ViewBag
            var studentWithScores = studentsInClass.Select(s => new
            {
                s.Id,
                s.Name,
                SubjectId = selectedClass.SubjectId,
                Mark = s.Scores.FirstOrDefault(score => score.SubjectId == selectedClass.SubjectId)?.Mark ?? -1 // Lấy điểm số cho môn học của lớp
            }).ToList();

            ViewBag.Students = studentsNotInClassItems;
            ViewBag.StudentInClass = studentWithScores;
            ViewBag.ClassId = Id;
            ViewBag.Name = selectedClass.Name;

            _logger.LogInformation("Class collected.", Id);

            return View();
        }


        // POST: /Class/AranageClass
        [HttpPost]
        public async Task<IActionResult> AddAranageClass(Enrollment enroll)
        {
            _logger.LogInformation("Received Student: {@student}", enroll); // Logging to check received data

            if (enroll.StudentId.HasValue)
            {
                Enrollment newEroll = new Enrollment
                {
                    ClassId = enroll.ClassId,
                    StudentId = enroll.StudentId,
                };
                _studentDbContext.Enrollments.Add(newEroll);
                await _studentDbContext.SaveChangesAsync();
            }
            else
            {
                TempData["InvalidData"] = "Please choose a student";
            }

            return RedirectToAction("AranageClass", "Class", new { Id = enroll.ClassId });
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST: /Class/UpdateScore
        [HttpPost]
        public async Task<IActionResult> UpdateScore(int studentId, double mark, int classId, int subjectId)
        {
            try
            {
                // Kiểm tra giá trị điểm số hợp lệ (0 <= mark <= 10)
                if (mark < 0 || mark > 10)
                {
                    return Json(new { success = false, error = "Mark must be between 0 and 10." });
                }

                var scoreToUpdate = await _studentDbContext.Scores
                    .SingleOrDefaultAsync(s => s.StudentId == studentId && s.SubjectId == subjectId);
                _logger.LogInformation("Class collected.", studentId);
                if (scoreToUpdate != null)
                {
                    // Nếu đã có điểm số, cập nhật lại
                    scoreToUpdate.Mark = mark;
                }
                else
                {
                    // Nếu chưa có điểm số, thêm mới
                    scoreToUpdate = new Score
                    {
                        StudentId = studentId,
                        SubjectId = subjectId,
                        Mark = mark
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

        // POST: Class/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                Class classToDelete = _studentDbContext.Classes.FirstOrDefault(x => x.Id == Id);
                if (classToDelete == null)
                {
                    return NotFound();
                }

                _studentDbContext.Classes.Remove(classToDelete);
                await _studentDbContext.SaveChangesAsync();

                TempData["DeleteClassSuccess"] = "Class deleted successfully.";

                // Redirect to the current page or specific action
                return RedirectToAction("Index", "Student");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // POST: Class/DeleteStudentOfClass
        [HttpPost]
        public async Task<IActionResult> DeleteStudentOfClass(int Id, int ClassId)
        {
            try
            {
                // Tìm bản ghi Enrollment dựa vào studentId và classId
                var enrollmentOfStudentToUpdate = await _studentDbContext.Enrollments
                    .FirstOrDefaultAsync(x => x.StudentId == Id && x.ClassId == ClassId);

                if (enrollmentOfStudentToUpdate == null)
                {
                    return NotFound();
                }

                // Cập nhật trường Status thành 0
                enrollmentOfStudentToUpdate.Status = 0;

                await _studentDbContext.SaveChangesAsync();


                // Redirect to the current page or specific action
                return RedirectToAction("AranageClass", "Class", new {Id = ClassId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
