using CMSProject.Data;
using CMSProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CMSProject.Controllers
{

    [Authorize]
    public class CreatorController(CmsbbddContext context) : Controller
    {
        private readonly CmsbbddContext _context = context;

        //Lleva a la vista "CreatorDashboard". También, cambia el estado de la vista dependiendo
        // de los filtros de busqueda utilizados
        public IActionResult CreatorDashboard(string? searchstring, string? date)
        {
            var util  = new Utils(_context, null);
            string? user = User.Identity!.Name;

            var publication = _context.PublicationTables
                .Include(s => s.IdUserNavigation)
                .Where(u => u.IdUserNavigation!.Name == user)
                .ToList();

            CmsViewModel viewModel = new CmsViewModel();

            viewModel.Publications = publication;

            viewModel = util.SearchFilter(searchstring, date, viewModel);

            return View(viewModel);
        }

        #region Create

        //Lleva a la vista "CreatePublication"
        public IActionResult CreatePublication()
        {
            return View();
        }

        //Crea una nueva publicación usando el titulo, la descripcion y la fecha de publicación
        //que fueron añadidas por el usuario. Además, agrega un registro de Creación al historial
        //del cliente 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePublication([Bind("Id, Title, Concept, PublicationDate")] PublicationTable publication)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var id = _context.UserTables.Where(i => i.Name == User.Identity!.Name).Select(x => x.Id).FirstOrDefault();
                    publication.IdUser = id;
                    _context.Add(publication);
                    await _context.SaveChangesAsync();

                    #region Añadir al historial
                    var lastModification = new LastModificationTable();

                    lastModification.Publication = publication.Title!.Trim();
                    lastModification.IdCreator = id;

                    lastModification.LastModification = DateTime.Now;
                    lastModification.Modification = "Creación";
                    await _context.LastModificationTables.AddAsync(lastModification);
                    _context.SaveChanges();
                    #endregion

                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Imposible guardar los cambios");
                }
                return RedirectToAction("CreatorDashboard", "Creator");
            }
            return View(publication);
        }

        #endregion

        #region Delete

        //Elimina una nueva publicación creada por el usuario. Además, agrega un registro de
        //"Borrado" al historial del cliente 
        public IActionResult DeletePublicationConfirmed(int id)
        {
            var publication = _context.PublicationTables.Find(id);

            #region Añadir al historial
            var lastModification = new LastModificationTable();

            lastModification.Publication = publication!.Title!.Trim();
            lastModification.IdCreator = publication.IdUser;

            lastModification.LastModification = DateTime.Now;
            lastModification.Modification = "Borrado";
            _context.LastModificationTables.Add(lastModification);
            _context.SaveChanges();
            #endregion

            if (publication == null)
                return NotFound();

            _context.PublicationTables.Remove(publication);
            _context.SaveChanges();

            return RedirectToAction("CreatorDashboard", "Creator");
        }
        #endregion

        #region Detail

        //Devuelve un archivo Json con los datos de la publicación creada por el usuario para
        // mostrarla con más detalle en una ventana modal 
        public IActionResult DetailsPublication(int? id)
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

        #endregion

        #region Edit

        //Lleva a la vista de edicion de la publicación
        public async Task<IActionResult> EditPublication(int? id)
        {
            var user = await _context.PublicationTables.FindAsync(id);
            var option = new JsonSerializerOptions();
            if (user == null)
                return NotFound();

            return View(user);
        }

        //Edita los campos de la publicacion creada por el usuario. Ademas, agrega un regisro
        // de "Editado en el historial del cliente"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPublication(PublicationTable publication)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var idUser = _context.UserTables
                        .Where(i => i.Name == User.Identity!.Name)
                        .Select(x => x.Id)
                        .FirstOrDefault();

                    publication.IdUser = idUser;

                    _context.PublicationTables.Update(publication);
                    _context.SaveChanges();

                    #region Añadir al historial
                    var lastModification = new LastModificationTable();

                    lastModification.Publication = publication.Title!.Trim();
                    lastModification.IdCreator = publication.IdUser;

                    lastModification.LastModification = DateTime.Now;
                    lastModification.Modification = "Editado";
                    _context.LastModificationTables.Add(lastModification);
                    _context.SaveChanges();
                    #endregion

                    return RedirectToAction("CreatorDashboard", "Creator");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Imposible guardar los cambios");
                }
            }
            return View(publication);
        }

        #endregion

    }

}
