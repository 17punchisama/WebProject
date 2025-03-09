namespace WebProject.Models
{
    public class Notification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool Type { get; set; }
        //public string Title { get; set; }
        public string PostName { get; set; }
        public bool Isread { get; set; } = false;

        public Guid? PostId { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
