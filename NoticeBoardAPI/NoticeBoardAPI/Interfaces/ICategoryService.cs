using NoticeBoardAPI.Common;
using NoticeBoardAPI.Models;

namespace NoticeBoardAPI.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse<IEnumerable<CategoryModel>>> GetAllCategoriesAsync();
        Task<ServiceResponse<IEnumerable<SubCategoryModel>>> GetAllSubCategoriesAsync();
        Task<ServiceResponse<IEnumerable<SubCategoryModel>>> GetSubCategoriesByCategoryIdAsync(int categoryId);
    }
}
