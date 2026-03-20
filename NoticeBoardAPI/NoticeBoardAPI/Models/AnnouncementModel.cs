using NoticeBoardAPI.Enums;

namespace NoticeBoardAPI.Models
{
    public class AnnouncementModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategory {  get; set; }
        public string Title { get; set; }  = string.Empty;
        public string? DescriptionOfNote { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public AnnouncementStatus AnnouncementStatus { get; set; } = AnnouncementStatus.Active;
       
    }
}
