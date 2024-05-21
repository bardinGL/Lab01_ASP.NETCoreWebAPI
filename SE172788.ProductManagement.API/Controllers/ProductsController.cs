using Microsoft.AspNetCore.Mvc;
using SE172788.ProductManagement.Repo.Models;
using SE172788.ProductManagement.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SE172788.ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts(string sortField, string sortOrder, string searchString, int? pageIndex, int? pageSize)
        {
            // Filtering
            Expression<Func<Product, bool>> filter = null;
            if (!string.IsNullOrEmpty(searchString))
            {
                filter = p => p.ProductName.Contains(searchString) || p.UnitPrice.ToString().Contains(searchString);
            }

            // Sorting
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null;
            if (!string.IsNullOrEmpty(sortField))
            {
                if (sortField.ToLower() == "name")
                {
                    orderBy = sortOrder == "desc" ? q => q.OrderByDescending(p => p.ProductName) : q => q.OrderBy(p => p.ProductName);
                }
                else if (sortField.ToLower() == "price")
                {
                    orderBy = sortOrder == "desc" ? q => q.OrderByDescending(p => p.UnitPrice) : q => q.OrderBy(p => p.UnitPrice);
                }
            }

            var products = _unitOfWork.Products.GetListById(filter, orderBy, pageIndex: pageIndex, pageSize: pageSize);
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _unitOfWork.Products.GetByID(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public ActionResult<Product> PostProduct(Product product)
        {
            _unitOfWork.Products.Insert(product);
            _unitOfWork.Complete();
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _unitOfWork.Products.Update(product);
            _unitOfWork.Complete();
            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _unitOfWork.Products.GetByID(id);

            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.Products.Delete(id);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}
