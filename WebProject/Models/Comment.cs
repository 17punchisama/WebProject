namespace WebProject.Models
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description {  get; set; }


        public Guid PostId { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
