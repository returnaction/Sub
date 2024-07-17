using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sub.Models.Entities.User.User;

namespace Sub.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var user = _mapper.Map<User>(request);
            var userCreateResult = await _userManager.CreateAsync(user, request.Password);

            await _userManager.AddToRoleAsync(user, "Member");

            return RedirectToAction("Index", "Home");
        }


    }
}
