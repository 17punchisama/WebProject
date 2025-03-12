using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Services
{
    public class BackgroundTask
    {
        private readonly IServiceProvider _serviceProvider;

        public BackgroundTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await DeleteExpiredPostsAsync();
                    //UpdateTestCounter();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in BackgroundTask: {ex.Message}");
                }

                // Wait until midnight
                var now = DateTime.UtcNow;
                var nextMidnight = now.Date.AddDays(1);
                var delay = nextMidnight - now;

                await Task.Delay(delay, cancellationToken);
            }
        }

        private async Task DeleteExpiredPostsAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MyAppContext>();
                var postService = scope.ServiceProvider.GetRequiredService<PostService>();

                var expiredPosts = await dbContext.Posts.Where(p => p.EndDate <= DateTime.UtcNow).ToListAsync();
                if (expiredPosts.Any())
                {
                    //dbContext.Posts.RemoveRange(expiredPosts);
                    //await dbContext.SaveChangesAsync();

                    foreach (var post in expiredPosts)
                    {
                        await postService.ClosePostsAsync(post);
                    }
                    Console.WriteLine($"{expiredPosts.Count} expired posts deleted.");
                }
            }
        }

        private void UpdateTestCounter()
        {
            NotificationController.TestCounter = (NotificationController.TestCounter + 1) % 2;
            Console.WriteLine($"_testCounter updated to: {NotificationController.TestCounter}");
        }
    }
}
