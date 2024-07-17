using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sub.Models.Entities.User.User;

namespace Sub.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly
        public IActionResult Index()
        {
            return View();
        }
    }
}
