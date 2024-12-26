using CMSProject.Data;
using CMSProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMSProject.Controllers
{
    public class ClientController(CmsbbddContext context) : Controller
    {
        private readonly CmsbbddContext _context = context;

        //Lleva a la vista "ClientDashboard". También, cambia el estado de la vista dependiendo
        // de los filtros de busqueda utilizados
        public IActionResult ClientDashboard(string? searchstring, string? name, string? date)
        {
            var util = new Utils(_context, null);
            string? user = User.Identity!.Name;

            var publication = _context.PublicationTables
                .Include(s => s.IdUserNavigation)
                .Where(u => u.IdUserNavigation!.IdClientNavigation!.Name == user)
                .ToList();


            ViewData["Creator"] = new SelectList(_context.UserTables.Where(n => n.IdRole == 2 && n.IdClientNavigation!.Name == user), "Id", "Name");

            CmsViewModel viewModel = new CmsViewModel();

            viewModel.Publications = publication;

            viewModel = util.SearchFilter(searchstring, name, date, viewModel);

            return View(viewModel);

        }

        //Lleva a la vista "lastModification". En ella se muestra un historial de todos los cambios hechos 
        //por los "Creadores" de un mismo cliente. Además, cambia el estado de la vista dependiendo
        // de los filtros de busqueda utilizados
        public IActionResult LastModification(string? searchstring, string? name, string? date)
        {
            var util = new Utils(_context, null);
            string? user = User.Identity!.Name;

            ViewData["Creator"] = new SelectList(_context.UserTables.Where(n => n.IdRole == 2 && n.IdClientNavigation!.Name == user), "Id", "Name");


            var lastModification = _context.LastModificationTables
                .Include(u => u.IdCreatorNavigation)
                .Where(c => c.IdCreatorNavigation!.IdClientNavigation!.Name == user)
                .ToList();

            lastModification = lastModification
                .OrderByDescending(s => s.LastModification)
                .ToList();



            CmsViewModel viewModel = new CmsViewModel();

            viewModel.LastModifications = lastModification;


            if (!string.IsNullOrEmpty(searchstring))
            {
                viewModel.LastModifications = viewModel.LastModifications
                    .Where(s => s.Publication.Contains(searchstring))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(name))
            {
                viewModel.LastModifications = viewModel.LastModifications
                    .Where(n => n.IdCreator.ToString() == name)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(date))
            {
                DateTime dateAsDateTime;
                if (DateTime.TryParse(date, out dateAsDateTime))
                {
                    viewModel.LastModifications = viewModel.LastModifications
                        .Where(n => n.LastModification.Date == dateAsDateTime)
                        .ToList();

                }
            }

            return View(viewModel);
        }

        //Devuelve un archivo Json con los datos de la publicación creada por el usuario para
        // mostrarla con más detalle en una ventana modal 
        [HttpGet]
        public IActionResult DetailPost(int id)
        {

            var publication = _context.PublicationTables.Find(id);

            var idUser = _context.UserTables.Where(i => i.Id == publication!.IdUser).FirstOrDefault();
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
