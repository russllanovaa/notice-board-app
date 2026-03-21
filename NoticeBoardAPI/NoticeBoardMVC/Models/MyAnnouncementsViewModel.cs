using NoticeBoardMVC.Enums;

namespace NoticeBoardMVC.Models
{
    public class MyAnnouncementsViewModel
    {
        public List<AnnouncementViewModel> Announcements { get; set; } = new();
        public int Total => Announcements.Count;
        public int Active => Announcements.Count(a => a.StatusOfNote == AnnouncementStatus.Active);
        public int InActive => Announcements.Count(a => a.StatusOfNote == AnnouncementStatus.Inactive);
    }
}
