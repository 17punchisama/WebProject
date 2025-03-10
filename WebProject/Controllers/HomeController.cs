using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyAppContext _context;

        public HomeController(ILogger<HomeController> logger, MyAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Post> posts = await _context.Posts
                                .Where(p => !p.IsClosed)
                                .Include(p => p.Owner)
                                .ToListAsync();
            return View(posts);
        }
        public async Task<IActionResult> Search(string input)
        {
            var result = await _context.Tags
                .Where(t => t.Name == input)
                .Include(t => t.PostTags)
                    .ThenInclude(pt => pt.Post) // Include related Post
                    .ThenInclude(p => p.Owner) // Include related Owner in Post
                 .Select(t => new
                {
                    TagPosts = t.PostTags.Select(pt => pt.Post).ToList(), // Navigate through PostTag
                })
                .FirstOrDefaultAsync();

            if (result != null)
            {
                foreach (var post in result.TagPosts)
                {
                    if (post != null)
                    {
                        await _context.Entry(post)
                        .Collection(p => p.ParticipantPosts)
                        .LoadAsync();

                        await _context.Entry(post)
                        .Collection(p => p.PostTags)
                        .LoadAsync();

                        foreach (var pt in post.PostTags)
                        {
                            await _context.Entry(pt)
                            .Reference(p => p.Tag)
                            .LoadAsync();
                        }
                    } 
                }
            }

            // If no matching tag is found, still get MatchingPosts separately
            List<Post> matchingPosts = await _context.Posts
                .Where(p => p.Title.Contains(input))
                .Include(p => p.Owner)
                .Include(p => p.ParticipantPosts)
                .Include (p => p.PostTags)
                    .ThenInclude(pp => pp.Tag)
                .ToListAsync();

            List<Post> tagPosts = result?.TagPosts ?? new List<Post>();
            ViewBag.InputString = input;
            return View((matchingPosts, tagPosts));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
