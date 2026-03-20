namespace NoticeBoardMVC.Models
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string NameOfSubCategory { get; set; } = string.Empty;
    }
}
