using System;
using System.Data.Entity;
using System.Linq;
using BusinessLayer.Model.Repository;
using KendoMvcDemo.Core.Persistence.Models;
using KendoMvcDemo.Core.ViewModels;

namespace KendoMvcDemo.Core.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dataContext;

        public ProductRepository()
        {

        }
        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public RepositoryOperationResult<IQueryable<Product>> Query()
        {
            return RepositoryOperationResult<IQueryable<Product>>.Ok(_dataContext.Products.AsQueryable());
        }

        public RepositoryOperationResult<Product> Update(Product item)
        {
            try
            {
                _dataContext.Entry(item).State = EntityState.Modified;
                _dataContext.SaveChanges();

                return RepositoryOperationResult<Product>.Ok();
            }
            catch (Exception ex)
            {
                return RepositoryOperationResult<Product>.Error(ex, item, "");
            }
        }

        public RepositoryOperationResult<Product> Delete(Product item)
        {
            try
            {
                _dataContext.Entry(item).State = EntityState.Deleted;

                _dataContext.SaveChanges();

                return RepositoryOperationResult<Product>.Ok(item);
            }
            catch (Exception ex)
            {
                return RepositoryOperationResult<Product>.Error(ex, item, "");
            }
        }

        public RepositoryOperationResult<Product> Create(Product item)
        {
            try
            {
                _dataContext.Products.Add(item);

                _dataContext.SaveChanges();

                return RepositoryOperationResult<Product>.Ok(item);
            }
            catch (Exception ex)
            {
                return RepositoryOperationResult<Product>.Error(ex, item, "");
            }
        }
    }
}
