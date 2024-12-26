using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMSProject.Models;

public partial class FileTable
{
    [Display(Name = "Archivo")]
    public int Id { get; set; }

    [Display(Name = "Nombre del archivo")]
    public string? FileName { get; set; }

    [Display(Name = "Tipo de archivo")]
    public string? Type { get; set; }

    [Display(Name = "Ruta del archivo")]
    public string? Path { get; set; }

    public int? IdPublication { get; set; }

    public virtual PublicationTable? IdPublicationNavigation { get; set; }
}
