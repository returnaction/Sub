using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sub.Models.Entities.Company.VM;
using Sub.Repository.CompanyRepository;
using System.Security.Claims;

namespace Sub.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
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
    }
}
