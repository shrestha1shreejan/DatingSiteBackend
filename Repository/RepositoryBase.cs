using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DataContext _repositoryContext { get; set; }

        #region Constructor

        public RepositoryBase(DataContext context)
        {
            _repositoryContext = context;
        }

        #endregion


        #region Implementation

        public IQueryable<T> FindAll()
        {
            return this._repositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._repositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this._repositoryContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            this._repositoryContext.Set<T>().Remove(entity);
        }        

        public void Update(T entity)
        {
            this._repositoryContext.Set<T>().Update(entity);
        }

        public async Task SaveAsync()
        {
            await this._repositoryContext.SaveChangesAsync();
        }

        #endregion

    }
}
