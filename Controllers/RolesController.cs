using Microsoft.AspNetCore.Mvc;
using Questionnaire.Models;
using Questionnaire.Services;
using System.Threading.Tasks;

namespace Questionnaire.Controllers
{
    public class RolesController : Controller
    {
        private readonly IManageRolees _manageRolees;
        public RolesController(IManageRolees manageRolees)
        {
            _manageRolees = manageRolees;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _manageRolees.GetRoles());
        }

        public async Task<IActionResult> Details(string Id)
        {
            if(Id == null)
                return NotFound();

            var role = await _manageRolees.GetRole(Id);
            if(role == null)
                return NotFound();

            return View(role);
        }
        [HttpGet]
        public   IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Claims")] Roles role)
        {
            if (ModelState.IsValid)
            {
                await _manageRolees.CreateRole(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

           var EditRole =  await _manageRolees.GetRole(id);
            if (EditRole == null)
                return NotFound();

            return View(EditRole);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Claims")] Roles role)
        {
            if (id != role.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var EditRole = await _manageRolees.UpdateRole(id, role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();
            var DeleteRole = await _manageRolees.GetRole(id);

            if(DeleteRole == null)
                return NotFound();
            return View(DeleteRole);

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(string id)
        {
            if(id == null)
                return NotFound();
        
               await _manageRolees.DeleteRole(id);
                return RedirectToAction(nameof(Index));
        }


    }
}
