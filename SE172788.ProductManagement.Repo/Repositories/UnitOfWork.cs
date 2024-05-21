using SE172788.ProductManagement.Repo.Data;
using SE172788.ProductManagement.Repo.Models;
using System;

namespace SE172788.ProductManagement.Repo.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<Category> _categoryRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public GenericRepository<Product> Products
        {
            get
            {
                return _productRepository ??= new GenericRepository<Product>(_context);
            }
        }

        public GenericRepository<Category> Categories
        {
            get
            {
                return _categoryRepository ??= new GenericRepository<Category>(_context);
            }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
