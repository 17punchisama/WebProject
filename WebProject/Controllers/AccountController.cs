using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProject.Models.Helper;
using WebProject.Models.ViewModels;
using WebProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;

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
                        ModelState.AddModelError(string.Empty, "รหัสผ่านผิด");
                        return View(model);
                    }
                }
                ModelState.AddModelError(string.Empty, "ไม่พบชื่อผู้ใช้");
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
                    Gender = model.Gender,
                    Year = model.Year,
                    ImgURL = model.ImageURL,
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(string Name, string Email, string PhoneNumber)
        {
            // ดึงข้อมูลผู้ใช้ที่กำลังล็อกอิน
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                // Retrieve the imageURL of the current user
                var currentImageUrl = user.ImgURL; // Assuming ImageURL is a property of User model
                Console.WriteLine($"Current Image URL: {currentImageUrl}");

                // Check if the ModelState is valid after getting the current user info
                if (ModelState.IsValid)
                {
                    Console.WriteLine("Check");
                    // Update user details
                    user.Name = Name;
                    user.Email = Email;
                    user.PhoneNumber = PhoneNumber;


                    // Update the user in the database
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            // Return to the Profile page if the user is null or ModelState is not valid
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> EditImgProfile(IFormFile ProfileImage)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null && ProfileImage != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(fileStream);
                }

                user.ImgURL = "/images/" + fileName;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Profile"); 
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return Content("Error updating image.");
        }





    }
}
