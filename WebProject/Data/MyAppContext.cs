using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;
using WebProject.Models.Helper;

namespace WebProject.Data
{
    public class MyAppContext : IdentityDbContext<User>
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<ParticipantPost> ParticipantPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Post>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PostTag>()
                .HasKey(pt => new { pt.PostId, pt.TagId });

            builder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PostTag>()
               .HasOne(pt => pt.Tag)
               .WithMany(p => p.PostTags)
               .HasForeignKey(pt => pt.TagId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ParticipantPost>()
                .HasKey(pp => new { pp.UserId, pp.PostId });

            builder.Entity<ParticipantPost>()
                .HasOne(pp => pp.Post)
                .WithMany(p => p.ParticipantPosts)
                .HasForeignKey(pp => pp.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete when Post is deleted

            builder.Entity<ParticipantPost>()
                .HasOne(pp => pp.User)
                .WithMany(u => u.ParticipantPosts)
                .HasForeignKey(pp => pp.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade delete when User is deleted

            builder.Entity<Comment>()
                .HasOne(c => c.User) 
                .WithMany() 
                .HasForeignKey(c => c.UserId) 
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<Comment>()
                .HasOne<Post>() 
                .WithMany(p => p.Comments) 
                .HasForeignKey(c => c.PostId) 
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
