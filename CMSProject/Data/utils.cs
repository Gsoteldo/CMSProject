using CMSProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMSProject.Data
{
    public class Utils(CmsbbddContext context, IWebHostEnvironment? environment) : Controller
    {
        private readonly CmsbbddContext _context = context;
        private readonly IWebHostEnvironment _environment = environment!;


        public List<FileTable> BrowseFiles(int idPublication)
        {
            var publication = _context.PublicationTables.Find(idPublication);
            var company = _context.UserTables.Where(i => i.Id == publication!.IdUser).Select(c => c.IdClientNavigation!.Name).FirstOrDefault();
            var creator = _context.UserTables.Where(i => i.Id == publication!.IdUser).Select(c => c.Name).FirstOrDefault();
            string uploadFolder;

            if (string.IsNullOrEmpty(company!.Trim()))
                uploadFolder = Path.Combine(_environment.WebRootPath, "uploads", creator!.Trim());
            else
                uploadFolder = Path.Combine(_environment.WebRootPath, "uploads", company.Trim());


            List<FileTable> files = ListFiles(uploadFolder);

            return files;
        }

        public List<FileTable> ListFiles(string uploadFolder)
        {
            List<FileTable> files = new List<FileTable>();
            string[] subdirectories;
            string[] filePath;

            subdirectories = Directory.GetDirectories(uploadFolder, "*", SearchOption.AllDirectories);
            foreach (var dir in subdirectories)
            {
                filePath = Directory.GetFiles(Path.Combine(dir));
                foreach (var file in filePath)
                {
                    files.Add(new FileTable
                    {
                        FileName = Path.GetFileName(file),
                        Id = _context.FileTables.Where(f => f.FileName == Path.GetFileName(file)).Select(i => i.Id).FirstOrDefault(),
                    });
                }
            }
            return files;
        }

        public CmsViewModel SearchFilter (string? searchstring, string? name, string? date, CmsViewModel viewModel)
        {

            if (!string.IsNullOrEmpty(searchstring))
            {
                viewModel.Publications = viewModel.Publications!
                    .Where(s => s.Title!.Contains(searchstring))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(name) && name != "0")
            {
                viewModel.Publications = viewModel.Publications!
                    .Where(n => n.IdUser.ToString() == name)
                    .ToList();
            }

            if (!string.IsNullOrEmpty(date))
            {
                DateOnly dateAsDateTime;
                if (DateOnly.TryParse(date, out dateAsDateTime))
                {
                    viewModel.Publications = viewModel.Publications!
                        .Where(n => n.PublicationDate == dateAsDateTime)
                        .ToList();

                }
            }

            return viewModel;
        }

        public CmsViewModel SearchFilter(string? searchstring, string? date, CmsViewModel viewModel)
        {

            if (!string.IsNullOrEmpty(searchstring))
            {
                viewModel.Publications = viewModel.Publications!
                    .Where(s => s.Title!.Contains(searchstring))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(date))
            {
                DateOnly dateAsDateTime;
                if (DateOnly.TryParse(date, out dateAsDateTime))
                {
                    viewModel.Publications = viewModel.Publications!
                        .Where(n => n.PublicationDate == dateAsDateTime)
                        .ToList();

                }
            }

            return viewModel;
        }

        public CmsViewModel SearchFilterUsers(string? searchstring, string? client, CmsViewModel viewModel)
        {

            if (!string.IsNullOrEmpty(searchstring))
            {
                viewModel.Users = viewModel.Users!
                    .Where(s => s.Name.Contains(searchstring))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(client) && client != "0")
            {
                viewModel.Users = viewModel.Users!
                    .Where(n => n.IdClient.ToString() == client)
                    .ToList();
            }

            return viewModel;
        }



    }
}
