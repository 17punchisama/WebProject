using Microsoft.AspNetCore.Identity;
using WebProject.Models.Helper;

namespace WebProject.Models
{
    public class User : IdentityUser
    {
        //Personal Info
        public string Name {  get; set; }
        public Gender Gender { get; set; }
        public int Year { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public virtual ICollection<ParticipantPost> ParticipantPosts { get; set; } = new HashSet<ParticipantPost>();
    }
}
