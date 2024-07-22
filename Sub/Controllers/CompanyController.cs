﻿using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// This is for litle pop up when you click on the list of companies
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetCompanyByIdInfo(int id)
        {
            
            var company = await _companyService.GetCompanyByIdAsync(id);
            return Json(company);
        }

        public async Task<IActionResult> EditCompany(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            // null check add later
          
            return View(company);
        }

        [HttpPost]
        public async Task<IActionResult> EditCompany(CompanyVM request)
        {
            ModelState.Remove(nameof(request.User));
            ModelState.Remove(nameof(request.Employees));

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _companyService.UpdateCompanyAsync(request);

            return RedirectToAction("GetCompanyById", new {id = request.Id});
        }

        // Invitations
        [HttpGet]
        public IActionResult Invite(int companyId)
        {
            ViewBag.CurrentCompanyId = companyId;
            return View(new InvitationVM { CompanyId = companyId});
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
            var user = await _userManager.GetUserAsync(User);
            await _invitationService.RespondToInvitationAsync(id, accept, user!.Email!);
            return RedirectToAction("MyInvitations");
        }
    }
}
