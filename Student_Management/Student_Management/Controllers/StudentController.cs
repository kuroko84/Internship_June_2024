using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management.DBContext;
using Student_Management.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Student_Management.Controllers
{

    public class StudentController : Controller
    {

        private readonly StudentDbContext _studentDbContext;
        private readonly ILogger<StudentController> _logger;
        private readonly IDistributedCache _distributedCache;

        public StudentController(StudentDbContext studentDbContext,
            ILogger<StudentController> logger,
            IDistributedCache distributedCache
            )
        {
            _studentDbContext = studentDbContext;
            _logger = logger;
            this._distributedCache = distributedCache;
        }
        //Load student from database async method
        private async Task<List<Student>> LoadStudentsFromDatabaseAsync()
        {
            return await _studentDbContext.Students
                .Include(cs => cs.ClassOfStudent)
                .Include(e => e.Enrollments)
                .ToListAsync();
        }
        //Update students in cache async method
        private async Task UpdateStudentCacheAsync(string message)
        {
            var cachekey = "studentList";

            List<Student> students;

            var serializedStudentList = await _distributedCache.GetStringAsync(cachekey);

            if (!string.IsNullOrEmpty(serializedStudentList))
            {
                _logger.LogInformation("Cache has values");
                students = await _studentDbContext.Students
                    .Include(cs => cs.ClassOfStudent)
                    .Include(e => e.Enrollments)
                    .ToListAsync();
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                serializedStudentList = JsonConvert.SerializeObject(students, settings);

                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                await _distributedCache.SetStringAsync(cachekey, serializedStudentList, options);
                _logger.LogInformation("Cache updated after " + message);
            }
        }

        //Get Student/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cachekey = "studentList";

            List<Student> students;

            var serializedStudentList = await _distributedCache.GetStringAsync(cachekey);

            if (!string.IsNullOrEmpty(serializedStudentList))
            {
                students = JsonConvert.DeserializeObject<List<Student>>(serializedStudentList);
                _logger.LogInformation("Cache has values");
            }
            else
            {
                students = await LoadStudentsFromDatabaseAsync();

                if (students == null)
                {
                    _logger.LogWarning("No students found in the database!");
                    students = new List<Student>();
                }

                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                serializedStudentList = JsonConvert.SerializeObject(students, settings);

                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                _logger.LogInformation("Cache hasn't any value");
                await _distributedCache.SetStringAsync(cachekey, serializedStudentList, options);
            }

            return View(students);
        }

        //GET: Student/More
        [HttpGet]
        public IActionResult More(int Id)
        {
            // Trang xem thêm thông tin
            Student newStudent = _studentDbContext.Students.SingleOrDefault(s => s.Id == Id);
            return View(newStudent);
        }

        //POST: Student/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            // Tìm sinh viên cần chỉnh sửa
            var existingStudent = await _studentDbContext.Students.SingleOrDefaultAsync(s => s.Id == student.Id);
            // Cập nhật thông tin
            existingStudent.Name = student.Name;
            existingStudent.DateOfBirth = student.DateOfBirth;

            // Lưu thay đổi
            _studentDbContext.Students.Update(existingStudent);

            // Lưu thay đổi vào cơ sở dữ liệu
            await _studentDbContext.SaveChangesAsync();

            // Update cache
            await UpdateStudentCacheAsync("edited");

            // Chuyển hướng đến action "More" của controller "Student" để hiển thị chi tiết sinh viên
            return RedirectToAction("Index", "Student");
        }


        public IActionResult AddStudent()
        {
            ViewBag.ClassOfStudents = new SelectList(_studentDbContext.ClassOfStudents.ToList(), "Id", "Name");
            return View();

        }

        // POST: Student/AddStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudent(Student student)
        {
            if (student.ClassOfStudentId.HasValue)
            {
                Student newStudent = new Student
                {
                    Name = student.Name,
                    DateOfBirth = student.DateOfBirth,
                    ClassOfStudentId = student.ClassOfStudentId,
                };
                _studentDbContext.Students.Add(newStudent);
                await _studentDbContext.SaveChangesAsync();
                // Update cache
                await UpdateStudentCacheAsync("added");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["InvalidData"] = "Please choose class";
            }

            // Add Viewbag for view
            ViewBag.ClassOfStudents = new SelectList(_studentDbContext.ClassOfStudents.ToList(), "Id", "Name");
            return View(student);
        }

        // POST: Student/Delete
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

                // Update cache
                await UpdateStudentCacheAsync("delete");

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
