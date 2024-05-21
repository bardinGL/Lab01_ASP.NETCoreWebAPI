using SE172788.ProductManagement.Repo.Models;
using System;
using System.Threading.Tasks;

namespace SE172788.ProductManagement.Repo.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<Product> ProductRepository { get; }
        GenericRepository<Category> CategoryRepository { get; }
        Task<int> Complete();
    }
}
