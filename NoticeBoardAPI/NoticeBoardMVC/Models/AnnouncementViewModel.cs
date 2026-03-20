namespace NoticeBoardMVC.Models
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string StatusOfNote { get; set; } = string.Empty;
        public string NameOfCategory { get; set; } = string.Empty;
        public string NameOfSubCategory {  get; set; } = string.Empty;
        public string DisplayName {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

    }
}
