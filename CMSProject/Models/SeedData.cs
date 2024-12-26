using Microsoft.EntityFrameworkCore;
using CMSProject.Data;

namespace CMSProject.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new CmsbbddContext(
                serviceProvider.GetRequiredService<DbContextOptions<CmsbbddContext>>());
            if (!context.RoleTables.Any())
            {
                context.RoleTables.AddRange(
                    new RoleTable
                    {
                        Role = "Admin"
                    },
                    new RoleTable
                    {
                        Role = "Creador"
                    },
                    new RoleTable
                    {
                        Role = "Cliente"
                    },
                    new RoleTable
                    {
                        Role = "Publicista"
                    }
                    );
            }

            else if (!context.UserTables.Any())
            {
                context.UserTables.AddRange(
                    new UserTable
                    {
                        Name = "Gabriel",
                        Email = "Gabriel@prueba.com",
                        Password = "1234",
                        IdRole = 2
                    },
                    new UserTable
                    {
                        Name = "Alejandro",
                        Email = "Alejandro@prueba.com",
                        Password = "5678",
                        IdRole = 2
                    },
                    new UserTable
                    {
                        Name = "Andres",
                        Email = "Andres@prueba.com",
                        Password = "abcd",
                        IdRole = 2
                    }
                    );
            }

            else if (!context.PublicationTables.Any())
            {
                context.PublicationTables.AddRange(
                new PublicationTable
                {
                    Title = "Publicacion 1",
                    Concept = "Esta es la Publicacion 1",
                    PublicationDate = DateOnly.Parse("2024-3-15"),
                    IdUser = 1
                },


                new PublicationTable
                {
                    Title = "Publicacion 2",
                    Concept = "Esta es la Publicacion 2",
                    PublicationDate = DateOnly.Parse("2024-4-22"),
                    IdUser = 1
                },

                new PublicationTable
                {
                    Title = "Publicacion 3",
                    Concept = "Esta es la Publicacion 3",
                    PublicationDate = DateOnly.Parse("2024-2-10"),
                    IdUser = 2
                },

                new PublicationTable
                {
                    Title = "Publicacion 4",
                    Concept = "Esta es la Publicacion 4",
                    PublicationDate = DateOnly.Parse("2024-1-20"),
                    IdUser = 1
                }
                );
            }
            else
                return;
            context.SaveChanges();
        }
    }
}
