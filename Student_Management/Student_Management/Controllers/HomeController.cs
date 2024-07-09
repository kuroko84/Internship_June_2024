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

        public IActionResult Index()
        {
            var classes = _studentDbContext
                .Courses
                .Include(c => c.Subject)
                .Include(c => c.Enrollments)
                .ToList();
            return View(classes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
