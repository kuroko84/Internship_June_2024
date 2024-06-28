using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management.DBContext;
using Student_Management.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Student_Management.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDbContext _studentDbContext;
        private readonly ILogger<StudentController> _logger;

        public StudentController(StudentDbContext studentDbContext, ILogger<StudentController> logger)
        {
            _studentDbContext = studentDbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Show students
            var students = _studentDbContext.Students
                .Include(s => s.Enrollments)
                .Include(e => e.Scores)
                .ToList();
            return View(students);
        }

        public IActionResult More(int Id)
        {
            // Trang xem thêm thông tin
            Student newStudent = _studentDbContext.Students.SingleOrDefault(s => s.Id == Id);
            return View(newStudent);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            // Tìm sinh viên cần chỉnh sửa
            var existingStudent = _studentDbContext.Students.SingleOrDefault(s => s.Id == student.Id);
            // Cập nhật thông tin
            existingStudent.Name = student.Name;
            existingStudent.DateOfBirth = student.DateOfBirth;

            _studentDbContext.Students.Update(existingStudent);

            // Lưu thay đổi vào cơ sở dữ liệu
            _studentDbContext.SaveChanges();

            // Chuyển hướng đến action "More" của controller "Student" để hiển thị chi tiết sinh viên
            return RedirectToAction("Index", "Student");
        }


        public IActionResult AddStudent()
        {
            return View();
        }

        // POST: AddStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudent(Student student)
        {
            _logger.LogInformation("Received Student: {@student}", student);
            // Nhiều khi ko cần thiết lawmcs cái Modelstate!!! LỖI
            if (ModelState.IsValid)
            {
                Student newStudent = new Student
                {
                    Name = student.Name,
                    DateOfBirth = student.DateOfBirth
                };
                _studentDbContext.Students.Add(newStudent);
                await _studentDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            _logger.LogWarning("ModelState is invalid. Errors: {@errors}", ModelState.Values);

            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    _logger.LogError("ModelState error: {ErrorMessage}", error.ErrorMessage);
                }
            }

            return View(student);
        }

        // POST: Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                // Tìm student để xóa
                Student studentToDelete = _studentDbContext.Students.FirstOrDefault(x => x.Id == Id);
                if (studentToDelete == null)
                {
                    return NotFound();
                }

                _studentDbContext.Students.Remove(studentToDelete);
                await _studentDbContext.SaveChangesAsync();

                TempData["DeleteStudentSuccess"] = "Student deleted successfully.";

                // Redirect to the current page or specific action
                return RedirectToAction("Index", "Student");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
