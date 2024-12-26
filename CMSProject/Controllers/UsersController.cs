using CMSProject.Data;
using CMSProject.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


namespace CMSProject.Controllers
{
    public class UsersController(CmsbbddContext context) : Controller
    {

        //Lleva a la vista "Login"
        public IActionResult Index()
        {
            return View();
        }

        //Comprueba si el usuario que ha hecho login existe o no. En el caso de que no exista 
        //el usuario, no va a permitir la entrada en la pagina. En el caso de que exista,
        //dependiendo del rol que tenga el usuario va a restringir el paso a las paginas a las
        //que no tenga permiso
        [HttpPost]
        public async Task<IActionResult> Index(UserTable _user)
        {
            UserLogic userLogic = new UserLogic(context);
            var user = userLogic.ValidateUser(_user.Email, _user.Password!);
            if (user != null && user.IsActivated == true)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name.Trim()),
                    new Claim(ClaimTypes.Email, user.Email.Trim()),
                    new Claim(ClaimTypes.Role, user.IdRoleNavigation!.Role.Trim())
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                switch (user.IdRoleNavigation.Role.Trim())
                {
                    case "Admin":
                        return RedirectToAction("Index", "Admin");
                    case "Creador":
                        return RedirectToAction("CreatorDashboard", "Creator");
                    case "Cliente":
                        return RedirectToAction("ClientDashboard", "Client");
                    case "Publicista":
                        return RedirectToAction("PublisherDashboard", "Publisher");
                    default:
                        return RedirectToAction("Index", "Home");
                }
            }
            else
                return View();
        }

        //Hace que el usuario salga de su cuenta en la pagina
        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Users");
        }


    }
}

