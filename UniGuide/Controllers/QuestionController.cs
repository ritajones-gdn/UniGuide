using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniGuide.Data;
using UniGuide.Models;

namespace UniGuide.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Show Ask Question Form - Only High School students
        [Authorize]
        public async Task<IActionResult> Ask()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.Role != "HighSchool")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Submit Question - Only High School students
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Ask(Question question)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.Role != "HighSchool")
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                question.UserId = user.Id;
                question.UserName = user.FullName;
                question.CreatedAt = DateTime.Now;
                _context.Questions.Add(question);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Question posted successfully ✔";
                return RedirectToAction("Index", "Home");
            }
            return View(question);
        }

        // Show Question Details
        public async Task<IActionResult> Details(int id)
        {
            var question = await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // Submit Answer - Only College students
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAnswer(int questionId, string content)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.Role != "College")
            {
                return RedirectToAction("Details", new { id = questionId });
            }

            var answer = new Answer
            {
                Content = content,
                QuestionId = questionId,
                UserId = user.Id,
                UserName = user.FullName,
                Major = user.Major,
                CreatedAt = DateTime.Now
            };

            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = questionId });
        }
    }
}