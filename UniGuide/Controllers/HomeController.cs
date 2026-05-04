using Microsoft.AspNetCore.Mvc;
using UniGuide.Data;
using Microsoft.EntityFrameworkCore;

namespace UniGuide.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string category)
        {
            var questions = _context.Questions
                .Include(q => q.Answers)
                .OrderByDescending(q => q.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                questions = questions.Where(q => q.Category == category);
            }

            ViewBag.SelectedCategory = category ?? "All";
            return View(await questions.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}