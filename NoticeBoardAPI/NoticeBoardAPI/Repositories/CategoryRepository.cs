using Dapper;
using NoticeBoardAPI.Interfaces;
using NoticeBoardAPI.Models;

namespace NoticeBoardAPI.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;
        public CategoryRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            using var connection = dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<CategoryModel>(
                "SELECT * FROM Categories"
            );
        }

        public async Task<IEnumerable<SubCategoryModel>> GetAllSubCategoriesAsync()
        {
            using var connection = dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<SubCategoryModel>(
                "SELECT * FROM SubCategories"
            );
        }

        public async Task<IEnumerable<SubCategoryModel>> GetSubCategoriesByCategoryIdAsync(int categoryId)
        {
            using var connection = dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<SubCategoryModel>(
                "SELECT * FROM SubCategories WHERE CategoryId = @CategoryId",
                new { CategoryId = categoryId }
            );
        } 
    }
}
