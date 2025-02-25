namespace WebProject.Models
{
    public class Comment
    {
        public string Id { get; set; }
        public string Description {  get; set; }


        public string UserId { get; set; }
        public User? user { get; set; }

        public string ParentCommentId {  get; set; }
        public Comment? ParentComment { get; set; }

        public List<Comment> ReplyComments { get; set; }
    }
}
