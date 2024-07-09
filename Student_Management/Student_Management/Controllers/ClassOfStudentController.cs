using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Student_Management.DBContext;
using Student_Management.Models;
using Student_Management.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;



namespace Student_Management.Controllers
{
    public class ClassOfStudentController : Controller
    {
        private readonly StudentDbContext _studentDbContext;
        private readonly ILogger<StudentController> _logger;
        private readonly IConverter _converter;
        private readonly ICompositeViewEngine _viewEngine;

        public ClassOfStudentController(StudentDbContext studentDbContext, 
            ILogger<StudentController> logger, 
            IConverter converter, 
            ICompositeViewEngine viewEngine)
        {
            _studentDbContext = studentDbContext;
            _logger = logger;
            _converter = converter;
            _viewEngine = viewEngine;
        }

        public IActionResult Index()
        {
            var ClassOfStudent = _studentDbContext.ClassOfStudents
                .Include(s => s.Students)
                .ToList();

            return View(ClassOfStudent);
        }

        public async Task<IActionResult> ViewMoreStudentInClass(int Id, bool filterPoor = false, bool filterAverage = false, bool filterGood = false, bool filterExcellent = false)
        {
            var students = await _studentDbContext.Students
               .Include(s => s.ClassOfStudent)
               .Include(s => s.Scores)
               .Where(s => s.ClassOfStudentId == Id)
               .ToListAsync();

            // Tạo một Dictionary để lưu trữ điểm trung bình của từng sinh viên
            Dictionary<int, double> studentAverages = new Dictionary<int, double>();

            foreach (var student in students)
            {
                var averageScore = student.Scores.Any() ? student.Scores.Select(score => score.Mark).Average() : 0;
                studentAverages.Add(student.Id, averageScore);
            }

            // Lọc sinh viên theo các tiêu chí đã chọn
            if (filterPoor)
            {
                students = students.Where(s => studentAverages[s.Id] < 5).ToList();
            }
            else if (filterAverage)
            {
                students = students.Where(s => studentAverages[s.Id] >= 5 && studentAverages[s.Id] <= 6.5).ToList();
            }
            else if (filterGood)
            {
                students = students.Where(s => studentAverages[s.Id] > 6.5 && studentAverages[s.Id] <= 8).ToList();
            }
            else if (filterExcellent)
            {
                students = students.Where(s => studentAverages[s.Id] > 8).ToList();
            }
            else
            {
                students = students.ToList();
            }

            // Truyền danh sách điểm trung bình qua ViewBag
            ViewBag.StudentAverages = studentAverages;
            ViewBag.ClassId = Id; // Truyền biến Id qua ViewBag

            return View(students);
        }

        public IActionResult AddNewClassOfStudent()
        {
            return View();
        }

        //Post /ClassOfStudent/AddNewClassOfStudent
        [HttpPost]
        public async Task<IActionResult> AddNewClassOfStudent(ClassOfStudent classOfStudent)
        {
            _logger.LogInformation("Received Student: {@student}", classOfStudent.Name);
            ClassOfStudent newClassOfStudent = new ClassOfStudent
            {
                Name = classOfStudent.Name,
                Description = classOfStudent.Description,
                SchoolYear = classOfStudent.SchoolYear
            };
            _studentDbContext.ClassOfStudents.Add(newClassOfStudent);
            await _studentDbContext.SaveChangesAsync();
            return RedirectToAction("Index", "ClassOfStudent");
        }

        public async Task<IActionResult> ViewStudentInfo(int Id) 
        {
            // Kết thông tin 3 bảng
            var student = await _studentDbContext.Students
                .Include(s => s.Scores)
                    .ThenInclude(s => s.Subject)
                .Include(s => s.ClassOfStudent) // Nạp ClassOfStudent
                .FirstOrDefaultAsync(s => s.Id == Id);

            if (student == null)
            {
                return NotFound(); // Xử lý trường hợp không tìm thấy sinh viên
            }

            // Kiểm tra và gán ClassName
            var className = student.ClassOfStudent != null ? student.ClassOfStudent.Name : "N/A";

            var studentInfo = new StudentInfoViewModel
            {
                Id = student.Id,
                Name = student.Name,
                ClassName = className, // Gán ClassName đã kiểm tra
                Scores = student.Scores.Select(score => new ScoreInfo
                {
                    SubjectName = score.Subject.Name,
                    Mark = score.Mark
                }).ToList(),
                AverageMark = student.Scores.Any() ? student.Scores.Average(score => score.Mark) : 0
            };

            return View(studentInfo);
        }
        public IActionResult ExportToPdf(int classId)
        {
            var students = _studentDbContext.Students
                .Include(s => s.ClassOfStudent)
                .Include(s => s.Scores)
                .Where(s => s.ClassOfStudentId == classId)
                .ToList();
            var classOfStudent = _studentDbContext.ClassOfStudents
                .FirstOrDefault(c => c.Id == classId);
            var className = classOfStudent.Name;
            var schoolYear = classOfStudent.SchoolYear;
            ViewBag.className = className;
            ViewBag.schoolYear = schoolYear;
            // Dữ liệu để truyền vào PDF
            var pdfData = new List<object[]>();
            int index = 1;
            foreach (var student in students)
            {
                var averageScore = CalculateAverageScore(student.Id);

                pdfData.Add(new object[] {
                index,
                student.Name,
                student.DateOfBirth.ToString("MM/dd/yyyy"),
                averageScore
            });
                index++;
            }

            // Render view to string
            var htmlContent = RenderViewToString("ExportToPdf", pdfData);

            // Convert to PDF using DinkToPdf
            var pdf = _converter.Convert(new HtmlToPdfDocument()
            {
                GlobalSettings = {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait,
            },
                Objects = {
                new ObjectSettings
                {
                    HtmlContent = htmlContent,
                }
            }
            });

            return File(pdf, "application/pdf", "ClassReport.pdf");
        }

        private double CalculateAverageScore(int studentId)
        {
            var scores = _studentDbContext.Scores
                .Where(sc => sc.StudentId == studentId)
                .Select(sc => sc.Mark)
                .ToList();

            if (scores.Any())
            {
                return scores.Average();
            }

            return 0.0;
        }

        private string RenderViewToString(string viewName, object model)
        {
            ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);

                if (viewResult.View != null)
                {
                    var viewContext = new ViewContext(
                        ControllerContext,
                        viewResult.View,
                        ViewData,
                        TempData,
                        writer,
                        new HtmlHelperOptions()
                    );

                    viewResult.View.RenderAsync(viewContext).GetAwaiter().GetResult();
                    return writer.GetStringBuilder().ToString();
                }
                else
                {
                    throw new ArgumentNullException($"View '{viewName}' not found");
                }
            }
        }

        private string RenderViewToString(string viewName, StudentInfoViewModel model)
        {
            ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);

                if (viewResult.View != null)
                {
                    var viewContext = new ViewContext(
                        ControllerContext,
                        viewResult.View,
                        ViewData,
                        TempData,
                        writer,
                        new HtmlHelperOptions()
                    );

                    viewResult.View.RenderAsync(viewContext).GetAwaiter().GetResult();
                    return writer.GetStringBuilder().ToString();
                }
                else
                {
                    throw new ArgumentNullException($"View '{viewName}' not found");
                }
            }
        }

        public IActionResult ExportStudentToPdf(int studentId)
        {
            var student = _studentDbContext.Students
                .Include(s => s.ClassOfStudent)
                .Include(s => s.Scores)
                    .ThenInclude(s => s.Subject)
                .FirstOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                return NotFound();
            }

            // Prepare data for PDF
            var studentInfo = new StudentInfoViewModel
            {
                Id = student.Id,
                Name = student.Name,
                ClassName = student.ClassOfStudent.Name,
                Scores = student.Scores.Select(score => new ScoreInfo
                {
                    SubjectName = score.Subject.Name,
                    Mark = score.Mark
                }).ToList(),
                AverageMark = student.Scores.Any() ? student.Scores.Average(score => score.Mark) : 0
            };

            // Render view to string
            var htmlContent = RenderViewToString("ExportStudentToPdf", studentInfo);

            // Convert to PDF using DinkToPdf
            var pdf = _converter.Convert(new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = htmlContent,
                    }
                }
            });

            return File(pdf, "application/pdf", "StudentReport.pdf");
        }


    }


}
