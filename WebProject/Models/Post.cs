using WebProject.Models.Helper;

namespace WebProject.Models
{
    public class Post
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public int ParticipantAmount { get; set; }
        public List<int> Years { get; set; } = [];
        public List<Gender> RequiredGenders { get; set; } = [];
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public bool IsClosed { get; set; } = false;


        public virtual ICollection<PostTag> PostTags { get; set; } = new HashSet<PostTag>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<ParticipantPost> ParticipantPosts { get; set; } = new HashSet<ParticipantPost>();

        public string OwnerId { get; set; }
        public virtual User? Owner { get; set; }
    }
}
