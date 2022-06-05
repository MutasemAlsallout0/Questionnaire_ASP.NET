using Microsoft.AspNetCore.Mvc;
using Questionnaire.Models;
using Questionnaire.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Questionnaire.Controllers
{
    public class UsersController : Controller
    {
        private readonly IManageUsers _manageUsers;
        private readonly IManageRolees _manageRoles;

        public UsersController(IManageUsers manageUsers, IManageRolees manageRoles)
        {
            _manageUsers = manageUsers;
            _manageRoles = manageRoles;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _manageUsers.GetUsers());
        }
        [HttpGet]
        public async Task<IActionResult> AssignRoles(string Id)
        {

            var user = await _manageUsers.GetUser(Id);
            var role = (await _manageRoles.GetRoles()).Select(x => x.Name).ToList();
            ViewBag.roles = role;
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRoles([Bind("Id,Name,Roles")] Users user )
        {
            await _manageUsers.AssignRoles(user);   
            return RedirectToAction(nameof(Index));
        }
    } 
}
