using GameForum.Data;
using GameForum.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameForum.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        protected GameForumContext GameForumContext { get; set; }

        public RepositoryBase(GameForumContext gameforumContext)
        {
            this.GameForumContext = gameforumContext;
        }

        public void Create(T entity)
        {
            this.GameForumContext.Set<T>().Add(entity);
            GameForumContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            this.GameForumContext.Set<T>().Remove(entity);
            GameForumContext.SaveChanges();
        }

        public IQueryable<T> FindAll()
        {
            return this.GameForumContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.GameForumContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            this.GameForumContext.Set<T>().Update(entity);
            GameForumContext.SaveChanges();
        }
    }
}
