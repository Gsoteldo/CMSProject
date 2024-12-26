//using AspNetCore;
using CMSProject.Data;
using CMSProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMSProject.Controllers
{
    public class PublisherController(CmsbbddContext context, IWebHostEnvironment environment) : Controller
    {
        private readonly CmsbbddContext _context = context;
        private readonly IWebHostEnvironment _environment = environment;

        //Lleva al Dashboard de los usuarios "Publisher", en él se muestran las distintas publicaciones 
        //hechas por todos los usuarios "Creadores" que estan relacionados con un mismo cliente.
        //Además, cambia el estado de la vista dependiendo de los filtros de busqueda utilizados
        public IActionResult PublisherDashboard(string? searchstring, string? name, string? date)
        {
            var util = new Utils(_context, _environment);
            //Se busca la compañia a la que pertenece el usuario
            var company = _context.UserTables
                .Where(i => i.Name == User.Identity!.Name)
                .Select(x => x.IdClientNavigation!.Name)
                .FirstOrDefault();

            //Se buscan todas las publicaciones que corresponden con la empresa
            var publication = _context.PublicationTables
                .Include(i => i.IdUserNavigation)
                .Where(p => p.IdUserNavigation!.IdClientNavigation!.Name == company)
                .ToList();

            CmsViewModel viewModel = new CmsViewModel
            {
                Publications = publication
            };
            string uploadFolder;
            //string[] subdirectories;

            //Comprobacion de buscar si el publicista esta en una compañia o no. 
            if (!string.IsNullOrEmpty(company))
                uploadFolder = Path.Combine(_environment.WebRootPath, "uploads", company.Trim());
            else
                uploadFolder = Path.Combine(_environment.WebRootPath, "uploads", User.Identity!.Name!.Trim());

            List<FileTable> files = util.ListFiles(uploadFolder);

            ViewData["Files"] = new SelectList(files, "Id", "FileName");
            ViewData["Creator"] = new SelectList(_context.UserTables.Where(n => n.IdRole == 2 && n.IdClientNavigation!.Name == company), "Id", "Name");

            viewModel = util.SearchFilter(searchstring, name, date, viewModel);

            return View(viewModel);
        }

        #region PostPublication
        public IActionResult PostPublication(int id)
        {
            var util = new Utils(_context, _environment);
            var publication = _context.PublicationTables.Find(id);
            var files = util.BrowseFiles(id);

            ViewData["Files"] = new SelectList(files, "Id", "FileName");

            var option = new JsonSerializerOptions();
            return Json(publication, option);
        }


        [HttpPost]
        public async Task<IActionResult> PostPublicationConfirmed(int id, int file)
        {
            var publication = _context.PublicationTables.Find(id);

            PostingTable post = new PostingTable();

            if (file != -1)
            {
                var selectedFile = await _context.FileTables.Where(f => f.Id == file).FirstOrDefaultAsync();

                post.File = selectedFile;
            }

            post.Publication = publication!;
            await _context.PostingTables.AddAsync(post);

            publication!.IsPost = true;
            _context.PublicationTables.Update(publication);
            await _context.SaveChangesAsync();

            return RedirectToAction("PublisherDashboard", "Publisher");
        }

        #endregion


        //Muestra la vista "PostDashboard, en ella se muestran todas las publicaciones que ya se publicaron"
        public IActionResult PostDashboard()
        {
            CmsViewModel model = new CmsViewModel();
            var company = _context.UserTables
                .Where(i => i.Name == User.Identity!.Name)
                .Select(x => x.IdClientNavigation!.Name)
                .FirstOrDefault();

            var publication = _context.PublicationTables
                .Include(i => i.IdUserNavigation)
                .Where(p => p.IdUserNavigation!.IdClientNavigation!.Name == company)
                .ToList();

            var post = _context.PostingTables
                .Include(p => p.Publication)
                .Include(f => f.File)
                .Where(p => p.Publication.IdUserNavigation!.IdClientNavigation!.Name == company!.Trim())
                .ToList();

            model.Postings = post;

            return View(model);
        }

        //Devuelve un archivo Json con los datos de la publicación creada por el usuario para
        // mostrarla con más detalle en una ventana modal 
        public IActionResult DetailPost(int id)
        {

            var publication = _context.PublicationTables.Find(id);

            var idUser = _context.UserTables
                .Where(i => i.Id == publication!.IdUser)
                .FirstOrDefault();

            publication!.IdUserNavigation = idUser;

            //Para evitar hacer referencias circulares con "UserTable"
            var option = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = Json(publication, option);

            return json;
        }




    }
}



