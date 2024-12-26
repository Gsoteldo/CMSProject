using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMSProject.Data
{
    public class BreadcrumbHelper
    {
        public static String GenerateBreadcrumbs(ViewContext viewContext)
        {
            var controller = viewContext.RouteData.Values["controller"]?.ToString();
            var action = viewContext.RouteData.Values["action"]?.ToString();

            var breadcrumbs = new List<string>
            {
                "<li class= 'breadcrumb-item'><a href='/'>Home</a></li>"
            };

            if (controller == "Admin")
            {
                if (action == "Index")
                    breadcrumbs.Add("<li class='breadcrumb-item active'>Dashboard</li>");
                else if (action == "UserManagement")
                    breadcrumbs.Add("<li class='breadcrumb-item active'>Usuarios</li>");
            }
            else if (controller == "Creator")
            {
                if (action == "CreatorDashboard")
                    breadcrumbs.Add("<li class='breadcrumb-item active'>Dashboard</li>");
            }
            else if (controller == "Client")
            {
                if (action == "ClientDashboard")
                    breadcrumbs.Add("<li class='breadcrumb-item active'>Dashboard</li>");
                else if (action == "LastModification")
                    breadcrumbs.Add("<li class='breadcrumb-item active'>Ult.Modificaciones</li>");
            }
            else if (controller == "Publisher")
            {
                if (action == "PublisherDashboard")
                    breadcrumbs.Add("<li class='breadcrumb-item active'>Dashboard</li>");
                else if (action == "PostDashboard")
                    breadcrumbs.Add("<li class='breadcrumb-item active'>Post</li>");
            }
            else if (controller == "File")
                breadcrumbs.Add("<li class='breadcrumb-item active'>Archivos</li>");

            return string.Join("", breadcrumbs);
        }
    }
}
