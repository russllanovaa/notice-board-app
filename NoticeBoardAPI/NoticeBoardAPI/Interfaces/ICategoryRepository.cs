using NoticeBoardAPI.Models;

namespace NoticeBoardAPI.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync();
        Task<IEnumerable<SubCategoryModel>> GetAllSubCategoriesAsync();
        Task<IEnumerable<SubCategoryModel>> GetSubCategoriesByCategoryIdAsync(int categoryId);
    }
}
