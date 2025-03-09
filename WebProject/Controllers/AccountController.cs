using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProject.Models.Helper;
using WebProject.Models.ViewModels;
using WebProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;

namespace WebProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyAppContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(MyAppContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home"); // Redirect to home page after successful login
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }
                ModelState.AddModelError(string.Empty, "User not found.");
                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();  // Logs out the user
            return RedirectToAction("Index", "Home");  // Redirect to the homepage after logout
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.GenderList = Enum.GetValues(typeof(Gender))
                                     .Cast<Gender>()
                                     .Select(g => new SelectListItem
                                     {
                                         Value = g.ToString(),
                                         Text = g.ToString()
                                     });
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.TelNumber,
                    Name = "User",
                    Gender = model.Gender
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home"); // Redirect after successful registration
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            ViewBag.GenderList = Enum.GetValues(typeof(Gender))
                                     .Cast<Gender>()
                                     .Select(g => new SelectListItem
                                     {
                                         Value = g.ToString(),
                                         Text = g.ToString()
                                     });
            return View(model);
        }

        [Authorize]
        [Route("Account/Profile")]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is not null)
            {
                return View(user);
            }
            return View();
        }
        [Route("Account/Profile/{id}")]
        public async Task<IActionResult> Profile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            return Content("User not found");
        }
        [Authorize]
        public IActionResult Activity()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> GetMyActivity()
        {
            var user = await _userManager.Users
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));
            if (user is not null)
            {
                var posts = user.Posts?.Select(p => new
                {
                    PostId = p.Id,          // Renaming Id -> PostId
                    Name = p.Title,    // Renaming Title -> PostTitle
                    Date = p.EndDate.ToString("yyyy-MM-dd"), // Renaming Content -> PostContent
                    Time = p.EndDate.ToString("HH:mm") // Renaming AuthorName -> AuthorUsername
                }).ToList();
                return Json(posts);
            }
            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> GetJoinedActivity()
        {
            var user = await _userManager.Users
                .Include(u => u.ParticipantPosts)
                    .ThenInclude(pp => pp.Post)
                        .ThenInclude(p => p.Owner)
                .FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));
            var posts = user.ParticipantPosts?.Select(pp => new
            {
                PostId = pp.Post.Id,          // Renaming Id -> PostId
                Name = pp.Post.Title,    // Renaming Title -> PostTitle
                Date = pp.Post.EndDate.ToString("yyyy-MM-dd"), // Renaming Content -> PostContent
                Time = pp.Post.EndDate.ToString("HH:mm"), // Renaming AuthorName -> AuthorUsername
                Owner = pp.Post.Owner.Name
            }).ToList();
            return Json(posts);
        }
    }
}
