using CMSProject.Data;
using CMSProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CMSProject.Controllers
{
    [Authorize]
    public class FileController(IWebHostEnvironment environment, CmsbbddContext context) : Controller
    {
        private readonly IWebHostEnvironment _environment = environment;

        private readonly CmsbbddContext _context = context;


        //Comprueba si el directorio existe. Si el usuario está relacionado a un cliente, buscará
        //el directorio dentro de la carpeta del cliente. Si no existe el directorio lo creará
        public string ExistDirectory (string user, string? company)
        {

            string uploadFolder;

            if (string.IsNullOrEmpty(company?.Trim()))
                uploadFolder = Path.Combine(_environment.WebRootPath, "uploads", user);

            else
                uploadFolder = Path.Combine(_environment.WebRootPath, "uploads", company.Trim(), user);

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            return uploadFolder;
        }

        //Crea una lista de todos los archivos ubicados dentro de un directorio
        public List<FileTable> ListFiles(string uploadFolder)
        {
            string[] filePath = Directory.GetFiles(Path.Combine(uploadFolder));
            List<FileTable> files = new List<FileTable>();
            foreach (var file in filePath)
            {
                files.Add(new FileTable
                {
                    FileName = Path.GetFileName(file)
                });
            }

            return files;
        }

        //Lleva a la vista principal de subida y descarga de archivos
        public IActionResult FileIndex()
        {
            string? user = User.Identity!.Name;
            if (User.Identity.IsAuthenticated)
            {

                var company = _context.UserTables
                .Where(i => i.Name == user)
                .Select(c => c.IdClientNavigation!.Name)
                .FirstOrDefault();

                string uploadFolder = ExistDirectory(user!, company);

                var files = ListFiles(uploadFolder);

                return View(files);
            }
            else
                return RedirectToAction("Index", "Users");
        }

        // Carga archivos dentro de la carpeta "upload" + "nombre de usuario" y guarda su ubicacion
        // dentro de la base de datos siempre y cuando exista el archivo y que pese menos de 2 MB
        [HttpPost]
        public async Task<IActionResult> FileIndex(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "Por favor, selecciona un archivo";
                return View("FileIndex");
            }

            if (file.Length > 2000000)
            {
                TempData["ErrorMessage"] = "Archivo demasiado grande, el archivo tiene que ser de menos de 2 MB";
                return View("FileIndex");
            }
           

            string? user = User.Identity!.Name;

            var company = _context.UserTables
                .Where(i => i.Name == user)
                .Select(c => c.IdClientNavigation!.Name)
                .FirstOrDefault();

            string uploadFolder = ExistDirectory(user!, company);

            FileTable newFile = new FileTable();


            string filename = Path.GetFileName(file.FileName);
            string fileSavePath = Path.Combine(uploadFolder, filename);

            using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var files = ListFiles(uploadFolder);

            newFile.FileName = filename;
            newFile.Path = fileSavePath;
            newFile.Type = file.ContentType;
            _context.FileTables.Add(newFile);
            await _context.SaveChangesAsync();

            ViewBag.Message = filename + " subido correctamente";

            return View(files);
        }

        //Permite la descarga de archivos ubicados en la carpeta upload + "nombre de usuario"
        public FileResult DownloadFile(string file)
        {
            string? user = User.Identity!.Name;

            string path = Path.Combine(this._environment.WebRootPath, "uploads", user!) + "/" + file;
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/octet-stream", file);
        }

    }
}
