using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMSProject.Models;
public partial class PostingTable
{
    [Key]
    public int Id { get; set; }

    public virtual PublicationTable Publication { get; set; }

    public virtual FileTable? File { get; set; }




    //[Display(Name = "Titulo de Publicación")]
    //public string PostTitle {  get; set; }      //Dependiente de PublicationTable

    //[Display(Name = "Contenido de la Publicación")]
    //public string PostDescription { get; set; } //Dependiente de PublicationTable

    //[Display(Name = "Creador")]
    //public string PostAuthor { get; set; }      //Dependiente de PublicationTable

    //public DateOnly PostDate { get; set; }

    //public string Media {  get; set; }          //Dependiente de FileTable


}
