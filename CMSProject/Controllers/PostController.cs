using CMSProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CMSProject.Controllers
{
    public class PostController(CmsbbddContext context) : Controller
    {
        private readonly CmsbbddContext _context = context;
        
        public IActionResult Index()
        {
            var publication = _context.PublicationTables.Include(i => i.IdUserNavigation).ToList();



            return View();
        }
    }
}
