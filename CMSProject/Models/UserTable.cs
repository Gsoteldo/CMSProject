using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSProject.Models;

public partial class UserTable
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    [Required]
    public string Name { get; set; } = null!;
    
    [Display(Name = "Correo")]
    [Required]
    public string Email { get; set; } = null!;
    
    [Display(Name = "Contraseña")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    [Display(Name = "Role")]
    public int IdRole { get; set; }

    public string? Salt { get; set; }
    
    [Display(Name = "Empresa")]
    public int? IdClient { get; set; }

    public virtual bool IsActivated {  get; set; }

    public virtual UserTable? IdClientNavigation { get; set; }

    public virtual RoleTable? IdRoleNavigation { get; set; } = null!;

    [NotMapped]
    [Display(Name = "Foto de Perfil")]
    public IFormFile? Imagen { get; set; }


    public virtual ICollection<UserTable> InverseIdClientNavigation { get; set; } = new List<UserTable>();

    public virtual ICollection<LastModificationTable> LastModificationTables { get; set; } = new List<LastModificationTable>();

    public virtual ICollection<PublicationTable> PublicationTables { get; set; } = new List<PublicationTable>();
}
