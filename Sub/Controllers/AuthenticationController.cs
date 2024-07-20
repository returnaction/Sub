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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
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

            if(request.ProfilePicutre is not null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profile_pictures");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + request.ProfilePicutre.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.ProfilePicutre.CopyToAsync(fileStream);
                }

                user.ProfilePicturePath = "/profile_pictures/" + uniqueFileName;
            }

            // later need to add is there is an error in creating a user
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

        public async Task<IActionResult> UserEdit()
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);

            var userEditVM = _mapper.Map<UserEditVM>(user);

            return View(userEditVM);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditVM request)
        {

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);

            if(user is null)
            {
                return NotFound();
            }

            _mapper.Map(request, user);

            if(request.ProfilePicutre != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.ProfilePicutre.FileName);
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profile_pictures");

                var filePath = Path.Combine(uploadsFolder, fileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.ProfilePicutre.CopyToAsync(fileStream);
                }

                user.ProfilePicturePath = fileName;
            }

            var updateResult = await _userManager.UpdateAsync(user);

            if (updateResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach(var error in updateResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(request);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}
