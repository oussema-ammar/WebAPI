using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("v1/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost, Authorize(Roles = "Client")]
        public IActionResult AddCategory(string name)
        {
            try
            {
                Category category = new Category()
                {
                    Name = name,
                    UserId = GetMe(),
                };
                _categoryRepository.AddCategory(category);
                return Ok("Category added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{clientId}"), Authorize(Roles = "Admin")]
        public IActionResult GetUserCategories(int id)
        {
            var categories = _categoryRepository.GetUserCategories(id);
            return Ok(categories);
        }

        [HttpGet("/users/me/categories"), Authorize(Roles = "Client")]
        public IActionResult GetMyCategories()
        {
            int id = GetMe();
            var categories = _categoryRepository.GetUserCategories(id);
            return Ok(categories);
        }

        [HttpGet("{categoryId}"), Authorize]
        public IActionResult GetCategory(int categoryId)
        {
            var category = _categoryRepository.GetCategory(categoryId);
            return Ok(category);
        }

        [HttpPut("{categoryId}"), Authorize]
        public IActionResult EditCategory(int id, string name)
        {
            _categoryRepository.UpdateCategory(id, name);
            return Ok("Category Updated.");
        }

        [HttpDelete, Authorize]
        public IActionResult DeleteCategory(int id)
        {
            _categoryRepository.DeleteCategory(id);
            return Ok();
        }

        private int GetMe()
        {
            var id = User.FindFirstValue("Id");
            return int.Parse(id);
        }
    }
}
