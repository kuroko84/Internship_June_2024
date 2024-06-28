using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management.DBContext;
using Student_Management.Models;

namespace Student_Management.Controllers
{
    public class SubjectController : Controller
    {
        private readonly StudentDbContext _studentDbContext;
        private readonly ILogger<StudentController> _logger;
        public SubjectController(StudentDbContext studentDbContext, ILogger<StudentController> logger)
        {
            _studentDbContext = studentDbContext;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var subjects = _studentDbContext.Subjects.Include(s => s.Classes).ToList();
            return View(subjects);
        }
        public IActionResult AddSubject()
        {
            return View();
        }

        // POST: AddStudent/AddStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewSubject(Subject subject)
        {
            _logger.LogInformation("Received Subject: {@subject}", subject); // Logging to check received data

            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    Subject newSubject = new Subject
                    {
                        Name = subject.Name,
                        Description = subject.Description,
                    };
                    _studentDbContext.Subjects.Add(newSubject);
                    await _studentDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            _logger.LogWarning("ModelState is invalid. Errors: {@errors}", ModelState.Values);

            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    _logger.LogError("ModelState error: {ErrorMessage}", error.ErrorMessage);
                }
            }

            return View(subject);
        }

        public IActionResult EditSubject(int Id) {
            Subject subject = _studentDbContext.Subjects.FirstOrDefault(x => x.Id == Id);
            return View(subject);
        }
        [HttpPost]
        public IActionResult EditSubject(Subject subject)
        {
            // Tìm môn cần chỉnh sửa trong cơ sở dữ liệu
            var existingSubject = _studentDbContext.Subjects.SingleOrDefault(s => s.Id == subject.Id);

            existingSubject.Name = subject.Name;
            existingSubject.Description = subject.Description;

            _studentDbContext.Subjects.Update(existingSubject);

            // Lưu thay đổi vào cơ sở dữ liệu
            _studentDbContext.SaveChanges();

            // Chuyển hướng đến action "More" của controller "Student" để hiển thị chi tiết sinh viên
            return RedirectToAction("Index", "Subject");
        }

        [HttpPost]
        // POST: Delete
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                Subject subjectToDelete = _studentDbContext.Subjects.FirstOrDefault(x => x.Id == Id);
                if (subjectToDelete == null)
                {
                    return NotFound();
                }

                _studentDbContext.Subjects.Remove(subjectToDelete);
                await _studentDbContext.SaveChangesAsync();

                TempData["DeleteSubjectSuccess"] = "Subject deleted successfully.";

                // Redirect to the current page or specific action
                return RedirectToAction("Index", "Subject");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
