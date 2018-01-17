using System;
using System.Linq;
using Autofac;
using BusinessLayer.Model.Repository;
using BusinessLayer.Model.UnitOfWork;

namespace KendoMvcDemo.Core.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private readonly IComponentContext _componentContext;

        public UnitOfWork(IComponentContext componentContext) : this(new DataContext(), componentContext)
        {

        }

        private UnitOfWork(DataContext dataContext, IComponentContext componentContext)
        {
            _dataContext = dataContext;
            _componentContext = componentContext;
        }

        public T GetRepository<T>() where T : IRepository
        {
            return _componentContext.Resolve<T>(new TypedParameter(typeof(DataContext), _dataContext));
        }

        public UnitOfWorkOperationResult Commit()
        {
            try
            {
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return UnitOfWorkOperationResult.Error(ex, "Data commited with error");
            }

            return UnitOfWorkOperationResult.Ok("Data commited successfully");
        }

        public UnitOfWorkOperationResult Rollback()
        {
            try
            {
                _dataContext
                    .ChangeTracker
                    .Entries()
                    .ToList()
                    .ForEach(x => x.Reload());
            }
            catch (Exception ex)
            {
                return UnitOfWorkOperationResult.Error(ex, "Data rollback with error");
            }

            return UnitOfWorkOperationResult.Ok("Data rollbacked successfully");
        }

        public void Dispose()
        {
            _dataContext?.Dispose();
        }
    }
}
