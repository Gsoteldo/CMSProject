namespace CMSProject.Models
{
    public class CmsViewModel
    {

        //Roles
        public List<RoleTable>? Roles { get; set; }
        public RoleTable? Role { get; set; }

        //Usuarios
        public List<UserTable>? Users { get; set; }
        public UserTable? User { get; set; }

        //Publicaciones
        public List<PublicationTable>? Publications { get; set; }
        public PublicationTable? Publication { get; set; }

        //Archivos
        public List<FileTable>? Files { get; set; }
        public FileTable? File { get; set; }

        //Ultimas Modificaciones
        public List<LastModificationTable>? LastModifications { get; set; }
        public LastModificationTable? LastModification { get; set; }

        //Posting
        public List<PostingTable> Postings { get; set; }
        public PostingTable? Posting { get; set; }


    }
}
