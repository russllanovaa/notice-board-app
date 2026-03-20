using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoticeBoardMVC.Enums;

namespace NoticeBoardMVC.Models
{
    public class CreateAnnouncementViewModel
    {
        [Required(ErrorMessage = "Оберіть категорію")]
        [Display(Name = "Категорія")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Оберіть підкатегорію")]
        [Display(Name = "Підкатегорія")]
        public int SubCategoryId {  get; set; }

        [Required(ErrorMessage = "Заголовок обов'язковий")]
        [MinLength(3, ErrorMessage = "Заголовок мінімум 3 символи")]
        [MaxLength(100, ErrorMessage = "Заголовок максимум 100 символів")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [MaxLength(250, ErrorMessage = "Опис максимум 250 символів")]
        [Display(Name = "Опис")]
        public string? DescriptionOfNote {  get; set; }

        [Display(Name = "Статус")]
        public AnnouncementStatus StatusOfNote { get; set; } = AnnouncementStatus.Active;

        public List<SelectListItem> Categories { get; set; } = new();
        public List<SelectListItem> SubCategories { get; set;} = new();
    }
}
