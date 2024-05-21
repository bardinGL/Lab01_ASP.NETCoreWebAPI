using Microsoft.AspNetCore.Mvc;
using SE172788.ProductManagement.Repo.Models;
using SE172788.ProductManagement.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult GetProducts(string sortBy = "ProductName", string sortOrder = "asc", string search = "")
        {
            Expression<Func<Product, bool>> filter = p => string.IsNullOrEmpty(search) || p.ProductName.Contains(search) || p.UnitPrice.ToString().Contains(search);
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null;

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortOrder.ToLower() == "asc")
                {
                    orderBy = query => query.OrderBy(p => EF.Property<object>(p, sortBy));
                }
                else if (sortOrder.ToLower() == "desc")
                {
                    orderBy = query => query.OrderByDescending(p => EF.Property<object>(p, sortBy));
                }
            }

            var products = _unitOfWork.ProductRepository.Get(filter, orderBy);
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _unitOfWork.ProductRepository.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null.");
            }

            _unitOfWork.ProductRepository.Insert(product);
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

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Complete();
            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _unitOfWork.ProductRepository.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}
