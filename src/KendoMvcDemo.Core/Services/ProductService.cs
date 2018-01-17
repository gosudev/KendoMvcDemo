using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using BusinessLayer.Model.Service;
using BusinessLayer.Model.UnitOfWork;
using KendoMvcDemo.Core.Persistence;
using KendoMvcDemo.Core.Persistence.Repositories;
using KendoMvcDemo.Core.ViewModels;

namespace KendoMvcDemo.Core.Services
{
    public class ProductService
    {
        private readonly IComponentContext _componentContext;
        public ProductService(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public ServiceOperationResult<IList<ProductViewModel>> Query()
        {

            using (IUnitOfWork uow = new UnitOfWork(_componentContext))
            {
                var repository = uow.GetRepository<IProductRepository>();

                return ServiceOperationResult<IList<ProductViewModel>>.Ok(repository.Query().Value.Select(x => new ProductViewModel()
                {
                    ProductId = x.ProductId,
                    Name = x.Name
                }).ToList());
            }
        }

        public ServiceOperationResult<ProductViewModel> Create(ProductViewModel model)
        {
            try
            {
                using (IUnitOfWork uow = new UnitOfWork(_componentContext))
                {
                    var repository = uow.GetRepository<IProductRepository>();
                    repository.Create(model.ConvertToDomainModel());

                    uow.Commit();

                    return ServiceOperationResult<ProductViewModel>.Ok(model, "");
                }
            }
            catch (Exception ex)
            {
                return ServiceOperationResult<ProductViewModel>.Error(ex, model, "");
            }
        }

        public ServiceOperationResult<ProductViewModel> Update(ProductViewModel model)
        {
            try
            {
                using (IUnitOfWork uow = new UnitOfWork(_componentContext))
                {
                    var repository = uow.GetRepository<IProductRepository>();
                    repository.Update(model.ConvertToDomainModel());

                    uow.Commit();

                    return ServiceOperationResult<ProductViewModel>.Ok(model, "");
                }
            }
            catch (Exception ex)
            {
                return ServiceOperationResult<ProductViewModel>.Error(ex, model, "");
            }
        }

        public ServiceOperationResult<ProductViewModel> Delete(ProductViewModel model)
        {
            try
            {
                using (IUnitOfWork uow = new UnitOfWork(_componentContext))
                {
                    var repository = uow.GetRepository<IProductRepository>();
                    repository.Delete(model.ConvertToDomainModel());

                    uow.Commit();

                    return ServiceOperationResult<ProductViewModel>.Ok(model, "");
                }
            }
            catch (Exception ex)
            {
                return ServiceOperationResult<ProductViewModel>.Error(ex, model, "");
            }
        }
    }
}
