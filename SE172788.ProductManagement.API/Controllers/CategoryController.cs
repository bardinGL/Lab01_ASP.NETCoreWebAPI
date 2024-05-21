using Microsoft.AspNetCore.Mvc;
using SE172788.ProductManagement.Repo.Models;
using SE172788.ProductManagement.Repo.Repositories;

namespace SE172788.ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoriesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Categories
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            return Ok(categories);
        }

        // POST: api/Categories
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if (category == null)
            {
                return BadRequest("Category is null.");
            }

            _unitOfWork.CategoryRepository.Insert(category);
            _unitOfWork.Complete();
            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public IActionResult ModifyCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest("Category ID mismatch.");
            }

            var existingCategory = _unitOfWork.CategoryRepository.GetByID(id);
            if (existingCategory == null)
            {
                return NotFound("Category not found.");
            }

            existingCategory.CategoryName = category.CategoryName;
            _unitOfWork.CategoryRepository.Update(existingCategory);
            _unitOfWork.Complete();
            return NoContent();
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public IActionResult RemoveCategory(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetByID(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Complete();
            return NoContent();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetByID(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
    }
}
