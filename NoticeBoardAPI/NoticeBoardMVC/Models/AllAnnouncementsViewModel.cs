using Microsoft.AspNetCore.Mvc.Rendering;

namespace NoticeBoardMVC.Models
{
    public class AllAnnouncementsViewModel
    {
        public List<AnnouncementViewModel> Announcements { get; set; } = new();
        public List<SelectListItem> Categories { get; set; } = new();
        public List<SelectListItem> SubCategories { get; set; } = new();
        public int? SelectedCategoryId { get; set; }
        public int? SelectedSubCategoryId { get; set; }
    }
}