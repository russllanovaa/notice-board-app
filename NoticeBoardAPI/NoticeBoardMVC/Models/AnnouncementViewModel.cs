namespace NoticeBoardMVC.Models
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? DescriptionOfNote { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string StatusOfNote { get; set; } = string.Empty;
        public string NameOfCategory { get; set; } = string.Empty;
        public string NameOfSubCategory { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}