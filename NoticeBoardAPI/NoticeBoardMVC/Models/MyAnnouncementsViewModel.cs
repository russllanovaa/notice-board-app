using NoticeBoardMVC.Enums;

namespace NoticeBoardMVC.Models
{
    public class MyAnnouncementsViewModel
    {
        public List<AnnouncementViewModel> Announcements { get; set; } = new();
        public int Total { get; set; }
        public int Active { get; set; }
        public int Inactive { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
    }
}