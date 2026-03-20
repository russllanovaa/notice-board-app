using Microsoft.AspNetCore.Identity;
using NoticeBoardAPI.Common;
using NoticeBoardAPI.Interfaces;
using NoticeBoardAPI.Models;

namespace NoticeBoardAPI.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<ServiceResponse<IEnumerable<CategoryModel>>> GetAllCategoriesAsync()
        {
            var categories = await categoryRepository.GetAllCategoriesAsync();
            return ServiceResponse<IEnumerable<CategoryModel>>.Ok(categories);
        }

        public async Task<ServiceResponse<IEnumerable<SubCategoryModel>>> GetAllSubCategoriesAsync()
        {
            var subCategories = await categoryRepository.GetAllSubCategoriesAsync();
            return ServiceResponse<IEnumerable<SubCategoryModel>>.Ok(subCategories);
        }

        public async Task<ServiceResponse<IEnumerable<SubCategoryModel>>> GetSubCategoriesByCategoryIdAsync(int categoryId)
        {
            var subCategories = await categoryRepository.GetSubCategoriesByCategoryIdAsync(categoryId);
            if (!subCategories.Any())
            {
                return ServiceResponse<IEnumerable<SubCategoryModel>>.Fail($"Підкатегорії для категорії з Id {categoryId} не знайдено");
            }
            return ServiceResponse<IEnumerable<SubCategoryModel>>.Ok(subCategories);
        }
    }
}
