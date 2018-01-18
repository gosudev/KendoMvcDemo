using System;
using System.Data;
using System.Data.Entity;
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
        private readonly bool _useTransaction;
        private readonly DbContextTransaction _transaction;

        public UnitOfWork(IComponentContext componentContext, bool useTransaction = false) : this(new DataContext(), componentContext, useTransaction)
        {

        }

        private UnitOfWork(DataContext dataContext, IComponentContext componentContext, bool useTransaction)
        {
            _dataContext = dataContext;
            _componentContext = componentContext;
            _useTransaction = useTransaction;

            if (_useTransaction)
            {
                if (_dataContext.Database.Connection.State != ConnectionState.Open)
                    _dataContext.Database.Connection.Open();

                _transaction = _dataContext.Database.BeginTransaction();
            }
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

                if (_useTransaction)
                    _transaction?.Commit();
            }
            catch (Exception ex)
            {
                if (_useTransaction)
                    _transaction?.Rollback();

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
            if (_useTransaction)
                _transaction?.Dispose();

            _dataContext?.Dispose();
        }
    }
}
