using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.Data;
using Questionnaire.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Questionnaire.Controllers
{
    [Authorize]

    public class QuestionController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public SignInManager<IdentityUser> _signInManager;
        readonly QuestionDbContext _context;
        public QuestionController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, QuestionDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [Authorize(Policy = "View")]
        public IActionResult Index()
        {
            return View(_context.QuestionsView.ToList());
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    var signInRusalt = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if(signInRusalt.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Wrong Password or email");
                    }
                }
                else
                {

                    ModelState.AddModelError("", "User Not Found");
                }
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var resalt = await _userManager.CreateAsync(new IdentityUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                },model.Password);
                if (resalt.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var item in resalt.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Policy = "Add")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult Create(CreateModel model)
        {
            var resalt = new QuestionsView()
            {
                QuestionText = model.Question,
                Option1 = model.Option1,
                Option2 = model.Option2,
                Option3 = model.Option3,
                InsertDate = DateTime.Now,
            };
            _context.QuestionsView.Add(resalt);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Policy ="Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var FindUser = _context.QuestionsView.FirstOrDefault(x => x.Id == id);
            if (FindUser == null)
                ModelState.AddModelError("", "User Not Found");
            return View();
        }
        [HttpPost]
        public IActionResult Edit(int id, CreateModel model)
        {
             
                var FindUser = _context.QuestionsView.FirstOrDefault(x => x.Id == id);
                if (FindUser == null)
                      ModelState.AddModelError("", "User Not Found");
                FindUser.QuestionText = model.Question;
                FindUser.Option1 = model.Option1;
                FindUser.Option2 = model.Option2;
                FindUser.Option3 = model.Option3;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            
           
        }
        [Authorize(Policy = "Delete")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var FindUser = _context.QuestionsView.FirstOrDefault(x => x.Id == id);
            if (FindUser == null)
                ModelState.AddModelError("", "User Not Found");
            return View();
        }
        [HttpGet("[Controller]/Delete/{id}")]
        public IActionResult PostDelete(int id)
        {
            var FindUser = _context.QuestionsView.FirstOrDefault(x => x.Id == id);
            if (FindUser == null)
                ModelState.AddModelError("", "User Not Found");

            _context.QuestionsView.Remove(FindUser);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
