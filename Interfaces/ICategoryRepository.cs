using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ICategoryRepository
    {
        public void AddCategory(Category category);
        public void DeleteCategory(int id);
        public void UpdateCategory(int Id, string name);
        public Category GetCategory(int id);
        public ICollection<Category> GetUserCategories(int userId);
        public ICollection<Category> GetCategories();

    }
}
