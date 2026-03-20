using NoticeBoardAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace NoticeBoardAPI.DTOs
{
    public class CreateAnnouncementRequest
    {
        [Required(ErrorMessage = "Категорія обов'язкова")]
        [Range(1, int.MaxValue, ErrorMessage = "Оберіть коректну категорію")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Підкатегорія обов'язкова")]
        [Range(1, int.MaxValue, ErrorMessage = "Оберіть коректну підкатегорію")]
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "Заголовок обов'язковий")]
        [MinLength(3, ErrorMessage = "Заголовок мінімум 3 символи")]
        [MaxLength(100, ErrorMessage = "Заголовок максимум 100 символів")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(250, ErrorMessage = "Опис максимум 250 символів")]
        public string? DescriptionOfNote { get; set; }
        public AnnouncementStatus StatusOfNote { get; set; } = AnnouncementStatus.Active;
    }
}
