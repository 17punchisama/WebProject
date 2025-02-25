using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;
using WebProject.Models.Helper;
using WebProject.Models.ViewModels;

namespace WebProject.Controllers
{
    public class PostController : Controller
    {

        private readonly MyAppContext _context;
        private readonly UserManager<User> _userManager;
        public PostController(MyAppContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            //Post post = new()
            //{
            //    Title = "test123",
            //    Description = "test nigga i love ในหลง",
            //    Years = new List<int> { 2, 3, 4 },
            //    RequiredGenders = new List<Gender> { Gender.Male, Gender.Female },
            //    EndDate = DateTime.Now,
            //    Owner = await _userManager.GetUserAsync(User)
            //};
            //post.PostTags.Add(new PostTag { Tag = new Tag { Name = "เยิ้มๆ" } });

            var model = await _context.Posts
                .Where(p => p.Id == id)
                .Include(p => p.PostTags) // Include the list of PostTag
                    .ThenInclude(pt => pt.Tag)
                .Include(p => p.ParticipantPosts)
                .Include(p => p.Owner)    // Include the User (Owner)
                .FirstOrDefaultAsync();
            return View(model);
        }

        //public IActionResult Index(string id)
        //{
        //    //get post by id
        //    //var model = _context.Posts.Where(p => p.Id == id);
        //    var model = "test";
        //    return View(model);
        //}

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = new CreatePostViewModel
            {
                Tags = await _context.Tags.ToListAsync()
            };
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.EndDate > DateTime.Now.AddDays(1))
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        Post post = new()
                        {
                            Title = model.Title,
                            Description = model.Description,
                            Years = model.Years,
                            RequiredGenders = model.RequiredGenders,
                            EndDate = model.EndDate,
                            OwnerId = user.Id
                        };
                        _context.Posts.Add(post);
                        foreach (var tagid in model.SelectedTagsId)
                        {
                            PostTag postTag = new()
                            {
                                TagId = tagid,
                                PostId = post.Id
                            };
                            _context.PostTags.Add(postTag);
                        }
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index", new { id = post.Id });
                    }

                }
                ModelState.AddModelError("EndDate", "วันปิดรับสมัครต้องเป็นวันพรุ่งนี้ขึ้นไป");

            }
            model.Tags = await _context.Tags.ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> RealTimeUpdate(Guid tagid)
        {
            Tag? tag = await _context.Tags.FindAsync(tagid);
            if (tag != null)
            {
                return Content(tag.Name);
            }
            return Content("none");
        }

        [Authorize]
        public async Task<IActionResult> Join(Guid postid)
        {
            var post = await _context.Posts
                    .Include(p => p.ParticipantPosts)
                    .FirstOrDefaultAsync(p => p.Id == postid);
            return RedirectToAction("Index", new {id = postid});
        }
    }
}
