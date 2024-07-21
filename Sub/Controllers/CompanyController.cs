using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sub.Models.Entities.Company.VM;
using Sub.Models.Entities.User.User;
using Sub.Repository.CompanyRepository;
using System.Security.Claims;

namespace Sub.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly UserManager<User> _userManager;

        public CompanyController(ICompanyService companyService, UserManager<User> userManager)
        {
            _companyService = companyService;
            _userManager = userManager;
        }

        // add new company
        public IActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(CompanyAddVM request)
        {
            request.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            ModelState.Remove("UserId");

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _companyService.AddCompanyAsync(request);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GetCompanies()
        {
            var userId = _userManager.GetUserId(User);
            var companies = await _companyService.GetListCompaniesOfUser(userId!);
            return View(companies);
        }

        public async Task<IActionResult> GetCompanyById(int companyId)
        {
            var company = await _companyService.GetCompanyByIdAsync(companyId);
            return Json(company);
        }
    }
}
