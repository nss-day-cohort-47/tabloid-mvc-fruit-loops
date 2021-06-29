using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();

        void AddCategory(Category category);

        void DeleteCategory(int categoryId);

        Category GetCategoryById(int id);

        void EditCategory(Category category);
    }
}