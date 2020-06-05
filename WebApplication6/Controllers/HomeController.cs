using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            var s = _context
                .Set<Student>()
                .Where(a => a.Id == 1)
                .Include(a => a.StudentsCourses)
                .ThenInclude(a => a.Course)
                .FirstOrDefault();
            return View(s);
        }

        public IActionResult AddDefault()
        {
            var s1 = _context.Set<Student>().FirstOrDefault(a => a.Id == 1);
            var s2 = _context.Set<Student>().FirstOrDefault(a => a.Id == 2);

            var c1 = _context.Set<Course>().FirstOrDefault(a => a.Id == 1);
            var c2 = _context.Set<Course>().FirstOrDefault(a => a.Id == 2);

            s1.StudentsCourses.Add(new StudentCourse
            {
                Course = c2,
                Student = s1
            });

            s2.StudentsCourses.Add(new StudentCourse
            {
                Course = c1,
                Student = s2
            });

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
