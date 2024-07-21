using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sub.Models.Entities.Company.VM;
using Sub.Models.Entities.Invitation;
using Sub.Models.Entities.User.User;
using Sub.Repository.CompanyRepository;
using Sub.Repository.InvitationRepository;
using System.Security.Claims;

namespace Sub.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IInvitationService _invitationService;
        private readonly UserManager<User> _userManager;


        public CompanyController(ICompanyService companyService, UserManager<User> userManager, IInvitationService invitationService)
        {
            _companyService = companyService;
            _userManager = userManager;
            _invitationService = invitationService;
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

        public async Task<IActionResult> GetCompanyById(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            return View(company);
        }

        public async Task<IActionResult> GetCompanyByIdInfo(int id)
        {
            
            var company = await _companyService.GetCompanyByIdAsync(id);
            return Json(company);
        }

        // Invitations
        [HttpGet]
        public IActionResult Invite()
        {
            return View(new InvitationVM());
        }

        [HttpPost]
        public async Task<IActionResult> Invite(InvitationVM model)
        {
            var userId = _userManager.GetUserId(User);
            await _invitationService.SendInvitationAsync(model, userId);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> MyInvitations()
        {
            //var user = _userManager.GetUserAsync(User);
            var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var invitations = await _invitationService.GetInvitationsForUserAsync(user.Email);
            return View(invitations);
        }

        [HttpPost]
        public async Task<IActionResult> RespondToInvitation(int id, bool accept)
        {
            await _invitationService.RespondToInvitationAsync(id, accept);
            return RedirectToAction("MyInvitations");
        }
    }
}
