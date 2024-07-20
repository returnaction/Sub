using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sub.Models.Entities.Employee.VM;
using Sub.Models.Entities.User.User;
using Sub.Repository.EmployeeRepository;
using System.Security.Claims;

namespace Sub.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeAddVM request)
        {
            request.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            ModelState.Remove("UserId");
            
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _employeeService.AddEmployeeAsync(request);

            return RedirectToAction("Index", "Home");
        }
    }
}
