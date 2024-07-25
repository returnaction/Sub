using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Sub.Models.Entities.User.User;
using Sub.Repository.EmaiRepository;

namespace Sub.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IEmailService _emailService;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IWebHostEnvironment webHostEnvironment, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _emailService = emailService;
        }


        // Sign up
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

            if (request.ProfilePicutre is not null)
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


        // Login logout
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
            if (hasUser is null)
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

        // Edit User
        public async Task<IActionResult> UserEdit()
        {
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);

            if (user is null)
            {
                return NotFound();
            }

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

            if (user is null)
            {
                return NotFound();
            }

            // store the old profile picture path before mapping

            var oldProfilePicturePath = user.ProfilePicturePath;

            _mapper.Map(request, user);

            if (request.ProfilePicture != null && request.ProfilePicture.Length > 0)
            {

                //deleting the old profile picture if it exists
                if (!string.IsNullOrEmpty(oldProfilePicturePath))
                {
                    var oldProfilePiturePath = Path.Combine(_webHostEnvironment.WebRootPath, "profile_pictures", oldProfilePicturePath);
                    if (System.IO.File.Exists(oldProfilePiturePath))
                    {
                        System.IO.File.Delete(oldProfilePiturePath);
                    }
                }
                var uniqueFileName = $"{Guid.NewGuid()}_{request.ProfilePicture.FileName}";
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profile_pictures");

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.ProfilePicture.CopyToAsync(fileStream);
                }

                user.ProfilePicturePath = uniqueFileName;
            }

            var updateResult = await _userManager.UpdateAsync(user);

            if (updateResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in updateResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(request);
        }


        // Forgot password

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswrodVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "Authentication", new { token, email = user.Email }, Request.Scheme);
            try
            {
                await _emailService.SendEmailAsync(user.Email, "Восстановление пароля", $"Щелкните по ссылке для восстановления пароля: <a href='{resetLink}'>link</a>");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка в отправке имейла.");
                return View(request);
            }

            return RedirectToAction("ForgotPasswordConfirmation");

        }


        // Reset password
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                // add error later
            }

            var model = new ResetPasswordVM { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}

