using System.ComponentModel.DataAnnotations;
using WebProject.Models.Helper;

namespace WebProject.Models.ViewModels
{
    public class CreatePostViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ParticipantAmount { get; set; }
        [Required]
        public List<int> Years { get; set; }
        [Required]
        public List<Gender> RequiredGenders { get; set; }
        public List<Tag> Tags { get; set; } = [];
        [Required]
        public List<Guid> SelectedTagsId { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string Location { get; set; }

    }
}
