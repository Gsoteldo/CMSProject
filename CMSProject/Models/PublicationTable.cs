using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMSProject.Models;

public partial class PublicationTable
{
    public int Id { get; set; }

    [Display(Name = "Titulo")]
    [Required]
    public string? Title { get; set; }

    [Display(Name = "Definición")]
    [Required]
    public string? Concept { get; set; }

    [Display(Name = "Fecha de Publicación")]
    [DataType(DataType.Date)]
    [Required]
    public DateOnly? PublicationDate { get; set; }

    [Display(Name = "Creador")]
    public int? IdUser { get; set; } = null!;

    public bool? IsPost { get; set; }

    public virtual ICollection<FileTable> FileTables { get; set; } = new List<FileTable>();

    public virtual UserTable? IdUserNavigation { get; set; }
}
