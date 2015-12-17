using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace timeBase
{
    public class baseRepository<T>: baseInterface<T> where T : class
    {
        protected timeContext ctx;
        protected DbSet<T> dbSet;

        public baseRepository(timeContext _ctx)
        {
            ctx = _ctx;
            dbSet = ctx.Set<T>();
        }

        public timeContext baseContext()
        {
            return ctx;
        }

        public T Get(int Id)
        {
            return dbSet.Find(Id);
        }

        public IQueryable<T> Get()
        {
            return dbSet;
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T oldEntity, T newEntity)
        {
            ctx.Entry(oldEntity).CurrentValues.SetValues(newEntity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Commit()
        {
            ctx.SaveChanges();
        }

    }
}
