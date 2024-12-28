using CMSProject.Data;
using CMSProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations;

namespace CMSProject.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController(IWebHostEnvironment environment, CmsbbddContext context) : Controller
    {

        private readonly CmsbbddContext _context = context;
        private readonly IWebHostEnvironment _environment = environment;

        //Lleva al Dashboard de los usuarios "Admin", en él se muestran las distintas publicaciones 
        //hechas por todos los usuarios "Creadores". Además, cambia el estado de la vista dependiendo
        // de los filtros de busqueda utilizados
        public IActionResult Index(string? searchstring, string? name, string? date)
        {
            var util = new Utils(_context, null);
            if (_context.PublicationTables == null)
                return Problem("Entity set is null");

            var publication = _context.PublicationTables
                .Include(s => s.IdUserNavigation).ToList();

            ViewData["Creator"] = new SelectList(_context.UserTables.Where(n => n.IdRole == 2), "Id", "Name");

            CmsViewModel model = new CmsViewModel
            {
                Publications = publication,
            };

            model = util.SearchFilter(searchstring, name, date, model);

            return View(model);
        }

        //Lleva a la vista "Gestión de Usuarios", en ella,  se muestran los distintos usuarios creados
        //por los Administradores. Se pueden observar tanto su nombre como su email, su rol en la pagina
        //y si están relacionados con algun cliente. Además, cambia el estado de la vista dependiendo
        // de los filtros de busqueda utilizados
        public IActionResult UserManagement(string? searchstring, string? name)
        {
            var util = new Utils(_context, null);

            ViewData["IdRole"] = new SelectList(_context.RoleTables, "Id", "Role");
            var client = _context.UserTables
                .Where(i => i.IdRoleNavigation!.Role == "Cliente")
                .ToList();

            ViewData["IdClient"] = new SelectList(client, "Id", "Name");


            var user = _context.UserTables
                .Include(s => s.IdRoleNavigation)
                .ToList();

            CmsViewModel model = new CmsViewModel
            {
                Users = user
            };

            model = util.SearchFilterUsers(searchstring, name, model);


            return View(model);
        }

        //(Pagina en Desuso) Lleva a la vista de Roles. En ella se observan los diferentes roles de la pagina
        //public IActionResult Roles()
        //{
        //    var role = _context.RoleTables
        //        .ToList();

        //    CmsViewModel model = new CmsViewModel
        //    {
        //        Roles = role
        //    };
        //    return View(model);
        //}


        #region Create User

        //(En desuso) Lleva a la pagina "Creacioón de usuarios"
        //public IActionResult CreateUser()
        //{

        //    ViewData["IdRole"] = new SelectList(_context.RoleTables, "Id", "Role");
        //    var client = _context.UserTables
        //        .Where(i => i.IdRoleNavigation!.Role == "Cliente")
        //        .ToList();

        //    ViewData["IdClient"] = new SelectList(client, "Id", "Name");
        //    return View();
        //}

        //Crea un nuevo Usuario en la base de datos seleccionando su nombre, email,
        //contraseña, rol, empresa (si es necesario) y foto de perfil

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserTable user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var hash = UserLogic.Hash(user.Password!);
                    user.Password = hash.Password;
                    user.Salt = hash.Salt;
                    user.IsActivated = true;

                    string uploadFolder;
                    uploadFolder = Path.Combine(_environment.WebRootPath, "userImages");


                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string filename = Path.GetFileName(user.Email + ".jpg");
                    string fileSavePath = Path.Combine(uploadFolder, filename);

                    using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
                    {
                        await user.Imagen!.CopyToAsync(stream);
                    }

                    await _context.AddAsync(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Imposible guardar los cambios");
                }
                return RedirectToAction("UserManagement", "Admin");
            }

            var client = _context.UserTables
                .Where(i => i.IdRoleNavigation!.Role == "Cliente")
                .Select(i => i.Name)
                .ToList();

            ViewData["IdRole"] = new SelectList(_context.RoleTables, "Id", "Role");
            ViewData["IdClient"] = new SelectList(client);
            return View(user);
        }

        #endregion

        #region Delete User


        //(En desuso) Lleva a la vista "DeleteUser"
        //public IActionResult DeleteUser(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var user = _context.UserTables
        //        .Include(s => s.IdRoleNavigation)
        //        .Include(s => s.IdClientNavigation)
        //        .FirstOrDefault(i => i.Id == id);


        //    if (user == null)
        //        return NotFound();

        //    return View(user);
        //}


        //Borra de manera logica a un usuario de la página, manteniendo sus publicaciones
        //y sus modificaciones. Además, no permite que el usuario pueda loguearse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUserConfirmed(int id)
        {
            var user = _context.UserTables.Find(id);
            if (user == null)
                return NotFound();


            user.IsActivated = false;

            _context.UserTables.Update(user);
            //_context.UserTables.Remove(user);
            //context.TablaSecundaria.RemoveRange(principal.TablaSecundaria);

            _context.SaveChanges();

            return RedirectToAction("UserManagement", "Admin");
        }

        #endregion

        #region Edit User

        //Devuelve un archivo Json con los datos del usuario para poder editarla
        //en una ventana modal 
        public async Task<IActionResult> GetEditUser(int? id)
        {
            var user = await _context.UserTables.FindAsync(id);
            var option = new JsonSerializerOptions();

            return Json(user, option);
        }

        //Edita el usuario seleccionado
        [HttpPost]
        public async Task<IActionResult> EditUser(UserTable user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var salt = _context.UserTables.Where(i => i.Email == user.Email).Select(x => x.Salt).FirstOrDefault();
                    if (user.Password == null)
                    {
                        user.Password = _context.UserTables.Where(i => i.Email == user.Email).Select(x => x.Password).FirstOrDefault();
                        user.Salt = salt;
                    }
                    else
                    {
                        var hash = UserLogic.Hash(user.Password);
                        user.Password = hash.Password;
                        user.Salt = hash.Salt;
                    }
                    string uploadFolder = Path.Combine(_environment.WebRootPath, "userImages");

                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    string filename = Path.GetFileName(user.Email.Trim() + ".jpg");
                    string fileSavePath = Path.Combine(uploadFolder, filename);

                    if (user.Imagen != null)
                    {
                        using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
                        {
                            await user.Imagen.CopyToAsync(stream);
                        }
                    }


                    _context.Update(user);
                    user.IsActivated = true;
                    _context.SaveChanges();
                    return RedirectToAction("UserManagement", "Admin");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Imposible guardar los cambios");
                }
            }
            var client = _context.UserTables
                .Where(i => i.IdRoleNavigation!.Role == "Cliente")
                .ToList();

            ViewData["IdClient"] = new SelectList(client, "Id", "Name");
            ViewData["IdRole"] = new SelectList(_context.RoleTables, "Id", "Role");
            return View(user);
        }

        #endregion

        #region Details

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

        //Devuelve un archivo Json con los datos del usuario para mostrarla con
        //más detalle en una ventana modal 
        public IActionResult DetailsUser(int? id)
        {
            if (id == null)
                return NotFound();

            var user = _context.UserTables
                .Include(s => s.IdRoleNavigation)
                .Include(s => s.IdClientNavigation)
                .FirstOrDefault(p => p.Id == id);

            return View(user);
        }

        #endregion


    }
}
