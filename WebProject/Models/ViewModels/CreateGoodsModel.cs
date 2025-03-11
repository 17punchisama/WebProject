using System.ComponentModel.DataAnnotations;
using WebProject.Models.Helper;
using System;
using System.Collections.Generic;

namespace WebProject.Models.ViewModels
{
    public class CreateGoodsModel
    {
        [Required(ErrorMessage = "กรุณากรอกชื่อกิจกรรม")]
        public string Title { get; set; }

        [Required(ErrorMessage = "กรุณากรอกคำโปรย")]
        public string Description { get; set; }

        [Required(ErrorMessage = "กรุณากรอกจำนวนคนเข้าร่วมกิจกรรม")]
        public int ParticipantAmount { get; set; }

        [Required(ErrorMessage = "กรุณาเลือกชั้นปี")]
        public List<int> Years { get; set; } = new List<int>(); // Initialize with empty list

        [Required(ErrorMessage = "กรุณาเลือกเพศ")]
        public List<Gender> RequiredGenders { get; set; } = new List<Gender>(); // Initialize with empty list

        public List<Tag> Tags { get; set; } = new List<Tag>(); // Initialize with empty list

        [Required(ErrorMessage = "กรุณาเลือกแท็ก")]
        public List<Guid> SelectedTagsId { get; set; } = new List<Guid>(); // Initialize with empty list

        [Required(ErrorMessage = "กรุณากรอกวันและเวลาของกิจกรรม")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "กรุณากรอกสถานที่")]
        public string Location { get; set; }
    }
}
