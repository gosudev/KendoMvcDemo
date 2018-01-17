using System.Linq;
using BusinessLayer.Model.Repository;
using KendoMvcDemo.Core.Persistence.Models;

namespace KendoMvcDemo.Core.Persistence.Repositories
{
    public interface IProductRepository : IRepository
    {
        RepositoryOperationResult<IQueryable<Product>> Query();
        RepositoryOperationResult<Product> Create(Product item);
        RepositoryOperationResult<Product> Update(Product item);
        RepositoryOperationResult<Product> Delete(Product item);
    }
}
