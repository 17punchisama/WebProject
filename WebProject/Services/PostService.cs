using System.Runtime.CompilerServices;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Services
{
    public class PostService
    {
        private readonly MyAppContext _context;

        public PostService(MyAppContext context)
        {
            _context = context;
        }

        public async Task ClosePostsAsync(Post post)
        {   
            post.IsClosed = true;
            await _context.Entry(post).Collection(p => p.ParticipantPosts).LoadAsync();
            foreach (var pp in post.ParticipantPosts)
            {
                Notification notification = new()
                {
                    Type = true,
                    PostName = post.Title,
                    PostId = post.Id,
                    UserId = pp.UserId
               
                };
                _context.Notifications.Add(notification);
            }
            await _context.SaveChangesAsync();
        }
    }
}
