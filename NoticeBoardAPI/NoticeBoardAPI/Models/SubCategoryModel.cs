namespace NoticeBoardAPI.Models
{
    public class SubCategoryModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string NameOfSubCategory { get; set; } = string.Empty;
    }
}
