using Microsoft.AspNetCore.Mvc.Rendering;

namespace NoticeBoardMVC.Models
{
    public class HomeViewModel
    {
        public List<AnnouncementViewModel> Announcements { get; set; } = new();
        public List<SelectListItem> Categories { get; set; } = new();
        public List<SelectListItem> SubCategories { get; set; } = new();
        public int? SelectCategoryId { get; set; }
        public int? SelectSubCategoryId { get; set; }
    }
}
