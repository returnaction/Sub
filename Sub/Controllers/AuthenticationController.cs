using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM request, string? returnUrl = null)
        {
            //returnUrl = returnUrl ?? Url.Action
            // so far let's use this link
            returnUrl = Url.Action("Index", "Home");

            var hasUser = await _userManager.FindByEmailAsync(request.Email);
            if(hasUser is null)
            {
                ModelState.AddModelError("FailedLogin", "Incorrect email or password");
                return View(request);
            }

            var loginResult = _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, true);

            if (loginResult.Result.Succeeded)
            {
                return Redirect(returnUrl!);
            }

            ModelState.AddModelError("FailedLogin", "Не правильный пароль или логин");
            return View(request);

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
