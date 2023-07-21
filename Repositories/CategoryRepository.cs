using InfluxDB.Client.Api.Domain;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlServerDataContext _context;
        public CategoryRepository(SqlServerDataContext context)
        {
            _context = context;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(t => t.Id == id)
            ?? throw new Exception("Category doesn't exist.");
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public ICollection<Category> GetCategories()
        {
            ICollection<Category> categories = _context.Categories.OrderBy(c => c.Id).ToList()
            ?? throw new Exception("No categories found");
            return categories;
        }

        public Category GetCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Category doesn't exist.");
            return category;
        }

        public ICollection<Category> GetUserCategories(int userId)
        {
            ICollection<Category> categories = _context.Categories
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Id).ToList();
            return categories;
        }

        public void UpdateCategory(int Id, string name)
        {
            var existingCategory = _context.Categories.FirstOrDefault(u => u.Id == Id);
            if (existingCategory != null)
            {
                existingCategory.Name = name;
                _context.SaveChanges();
            }
        }
    }
}