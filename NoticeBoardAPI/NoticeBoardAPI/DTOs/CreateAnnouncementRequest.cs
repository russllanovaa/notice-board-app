namespace NoticeBoardAPI.DTOs
{
    public class CreateAnnouncementRequest
    {
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? DescriptionOfNote { get; set; }
        public string StatusOfNote { get; set; } = "active";
    }
}
