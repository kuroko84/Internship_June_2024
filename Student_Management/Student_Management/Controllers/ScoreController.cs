﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management.DBContext;
using Student_Management.Models;

namespace Student_Management.Controllers
{
    public class ScoreController : Controller
    {
        private readonly StudentDbContext _studentDbContext;

        public ScoreController(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }
        public IActionResult Index()
        {
            var scores = _studentDbContext.Scores
                .Include(c => c.Student)
                .Include(c => c.Subject)
                .Include(c => c.Class)
                .ToList();
            return View(scores);
        }
        public IActionResult Grading()
        {
            return View();
        }
        // POST: Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int classId, int studentId, int subjectId)
        {
            try
            {
                Score scoreToDelete = await _studentDbContext.Scores
                    .SingleOrDefaultAsync(x => x.ClassId == classId && x.StudentId == studentId && x.SubjectId == subjectId);

                if (scoreToDelete == null)
                {
                    return NotFound();
                }

                _studentDbContext.Scores.Remove(scoreToDelete);
                await _studentDbContext.SaveChangesAsync();

                TempData["DeleteSuccess"] = "Score deleted successfully.";

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
