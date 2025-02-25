namespace WebProject.Models.Helper
{
    public class ParticipantPost
    {
        public string UserId {  get; set; }
        public virtual User? User { get; set; }

        public Guid PostId { get; set; }
        public virtual Post? Post { get; set; }
    }
}
