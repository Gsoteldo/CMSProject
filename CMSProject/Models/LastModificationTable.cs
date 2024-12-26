using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMSProject.Models;

public partial class LastModificationTable
{
    public int Id { get; set; }

    [Display(Name = "Creador")]
    public int? IdCreator { get; set; }

    [Display(Name = "Publicación")]
    public string Publication { get; set; } = null!;

    [Display(Name = "Ultima Modificación")]
    [DataType(DataType.DateTime)]
    public DateTime LastModification { get; set; }

    [Display(Name = "Modificación")]
    public string Modification { get; set; } = null!;

    public virtual UserTable? IdCreatorNavigation { get; set; } = null!;
}
